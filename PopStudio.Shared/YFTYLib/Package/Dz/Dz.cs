using PopStudio.Plugin;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Threading.Tasks;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.Package.Dz
{
    /// <summary>
    /// Marmalade SDK - Derbh API
    /// dzip archive
    /// It can use dz, gzip, lzma, bzip2, store and zero to save the file
    /// dz compression is only in Derbh API and dzip.exe
    /// I can't support it, so the program will only copy it out.
    /// </summary>
    internal class Dz
    {
        public static void Unpack(YFFile inFile, YFDirectory outFolder, bool changeimage, bool delete)
        {
            List<Task> decodeImage = null;
            if (changeimage)
            {
                decodeImage = new List<Task>();
            }
            using (IDisposablePool pool = new IDisposablePool())
            {
                using (BinaryStream bs = inFile.OpenAsBinaryStream())
                {
                    byte[] buffer = new byte[81920];
                    bs.Encode = EncodeHelper.ANSI;
                    YFFile tempName;
                    DtrzInfo dz = new DtrzInfo();
                    dz.Read(bs);
                    // ReadArchives
                    int archivesCount = dz.ArchivesCount;
                    BinaryStream[] bsLib = new BinaryStream[archivesCount];
                    for (int i = 0; i < archivesCount; i++)
                    {
                        if (dz.ArchiveNameLibrary[i] == null)
                        {
                            bsLib[i] = bs;
                        }
                        else
                        {
                            bsLib[i] = pool.Add(inFile.Parent.GetYFFile(dz.ArchiveNameLibrary[i]).OpenAsBinaryStream());
                        }
                    }
                    int chunksCount = dz.ChunksCount;
                    BinaryStream tempbs;
                    string adds = outFolder.Name + "/";
                    for (int i = 0; i < chunksCount; i++)
                    {
                        ChunkInfo SubInfo = dz.Chunks[i];
                        tempbs = bsLib[SubInfo.ArchiveIndex];
                        string nName = dz.FileNameLibrary[SubInfo.FileNameIndex];
                        if (SubInfo.MultiIndex != 0)
                        {
                            //Multi saved file
                            int index = nName.IndexOf('.');
                            if (index >= 0)
                            {
                                string n = nName[..index];
                                string ex = nName[index..];
                                nName = $"{n}_multi_{SubInfo.MultiIndex}{ex}";
                            }
                            else
                            {
                                nName = $"{nName}_multi_{SubInfo.MultiIndex}";
                            }
                        }
                        string newname = adds + dz.FolderNameLibrary[SubInfo.FolderNameIndex] + "/" + nName;
                        newname = newname.Replace('\\', '/').Replace("//", "/");
                        tempName = CreateYFFileFromPath(newname, outFolder);
                        tempbs.Position = SubInfo.Offset;
                        if (SubInfo.ZSize_For_Compress == -1)
                        {
                            SubInfo.ZSize_For_Compress = (int)(tempbs.Length - tempbs.Position);
                        }
                        DzCompressionFlags flags = SubInfo.Flags;
                        if ((flags & DzCompressionFlags.DZ) != 0)
                        {
                            //dz decompress
                            //unsupported
                            //just copy
                            using (BinaryStream bs2 = tempName.CreateAsBinaryStream())
                            {
                                tempbs.StaticCopyTo(bs2, SubInfo.ZSize_For_Dz, buffer);
                            }
                        }
                        else if ((flags & DzCompressionFlags.ZLIB) != 0)
                        {
                            //gzip decompress
                            using (BinaryStream bs2 = new BinaryStream())
                            {
                                tempbs.StaticCopyTo(bs2, SubInfo.ZSize_For_Compress, buffer);
                                bs2.Position = 0;
                                using (BinaryStream bs3 = tempName.CreateAsBinaryStream())
                                {
                                    using (GZipStream gZipStream = new GZipStream(bs2, CompressionMode.Decompress))
                                    {
                                        gZipStream.StaticCopyTo(bs3, buffer);
                                    }
                                }
                            }
                        }
                        else if ((flags & DzCompressionFlags.BZIP) != 0)
                        {
                            //bzip2 decompress
                            //using (BinaryStream bs2 = new BinaryStream())
                            //{
                            //    tempbs.CopyTo(bs2, SubInfo.ZSize_For_Compress);
                            //    bs2.Position = 0;
                            //    using (BinaryStream bs3 = new BinaryStream(tempName, FileMode.Create))
                            //    {
                            //        using (Ionic.BZip2.BZip2InputStream bZip2Stream = new Ionic.BZip2.BZip2InputStream(bs2))
                            //        {
                            //            bZip2Stream.CopyTo(bs3);
                            //        }
                            //    }
                            //}
                            throw new NotImplementedException();
                        }
                        else if ((flags & DzCompressionFlags.ZERO) != 0)
                        {
                            //zero chunk
                            int copytimes = SubInfo.Size / 81920;
                            int copyelse = SubInfo.Size % 81920;
                            byte[] temp = new byte[81920];
                            using (BinaryStream bs2 = tempName.CreateAsBinaryStream())
                            {
                                for (int j = 0; j < copytimes; j++)
                                {
                                    bs2.Write(temp, 0, 81920);
                                }
                                if (copyelse != 0)
                                {
                                    bs2.Write(temp, 0, copyelse);
                                }
                            }
                        }
                        else if ((flags & DzCompressionFlags.STORE) != 0)
                        {
                            //copy only
                            using (BinaryStream bs2 = tempName.CreateAsBinaryStream())
                            {
                                tempbs.StaticCopyTo(bs2, SubInfo.Size, buffer);
                            }
                        }
                        else if ((flags & DzCompressionFlags.LZMA) != 0)
                        {
                            //lzma decompress
                            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
                            byte[] properties = tempbs.ReadBytes(5);
                            long fileLength = tempbs.ReadInt64();
                            coder.SetDecoderProperties(properties);
                            using (BinaryStream bs3 = tempName.CreateAsBinaryStream())
                            {
                                coder.Code(tempbs, bs3, SubInfo.ZSize_For_Compress - 13, fileLength, null);
                            }
                        }
                        else
                        {
                            //unknow chunk
                            //copy only
                            using (BinaryStream bs2 = tempName.CreateAsBinaryStream())
                            {
                                tempbs.StaticCopyTo(bs2, SubInfo.Size, buffer);
                            }
                        }
                        if (changeimage)
                        {
                            string ex = tempName.GetExtension().ToLower();
                            if (ex == ".tex")
                            {
                                decodeImage.Add(Task.Run(() =>
                                {
                                    YFAPI.DecodeImage(tempName, tempName.Parent.CreateYFFile(tempName.GetNameWithoutExtension() + ".png"), 3);
                                    if (delete)
                                    {
                                        tempName.DeleteSelf();
                                    }
                                }));
                            }
                            else if (ex == ".txz")
                            {
                                decodeImage.Add(Task.Run(() =>
                                {
                                    YFAPI.DecodeImage(tempName, tempName.Parent.CreateYFFile(tempName.GetNameWithoutExtension() + ".png"), 4);
                                    if (delete)
                                    {
                                        tempName.DeleteSelf();
                                    }
                                }));
                            }
                        }
                    }
                }
            }
            if (changeimage && decodeImage is not null)
            {
                Task.WaitAll(decodeImage.ToArray());
            }
        }

        public static void Pack(YFDirectory inFolder, YFFile outFile, Func<string, DzCompressionFlags> func)
        {
            byte[] buffer = new byte[81920];
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                bs.Encode = EncodeHelper.ANSI;
                YFFile[] files = inFolder.GetAllFilesWithSubDirectoryFiles();
                string[] fileName = new string[files.Length];
                string[] pathName = new string[files.Length];
                StringPool folderPool = new StringPool();
                folderPool.ThrowInPool(string.Empty);
                int adds = inFolder.Name.Length + 2;
                for (int i = 0; i < files.Length; i++)
                {
                    fileName[i] = files[i].Name;
                    pathName[i] = files[i].Parent == inFolder ? string.Empty : files[i].GetPath(inFolder)[adds..^(files[i].Name.Length + 1)].Replace('/', '\\');
                    folderPool.ThrowInPool(pathName[i]);
                }
                DtrzInfo dz = new DtrzInfo();
                dz.FileNameNumber = (ushort)files.Length;
                dz.FolderNameNumber = (ushort)folderPool.Length;
                dz.FileNameLibrary = fileName;
                dz.FolderNameLibrary = new string[folderPool.Length];
                for (int i = 0; i < folderPool.Length; i++)
                {
                    dz.FolderNameLibrary[i] = folderPool[i].Value;
                }
                dz.Chunks = new ChunkInfo[files.Length];
                for (ushort i = 0; i < files.Length; i++)
                {
                    dz.Chunks[i] = new ChunkInfo((ushort)folderPool[pathName[i]].Index, i, i);
                }
                dz.WritePart1(bs);
                long backupOffset = bs.Position;
                bs.Position += files.Length << 4;
                DzCompressionFlags flags;
                for (int i = 0; i < files.Length; i++)
                {
                    ChunkInfo SubInfo = dz.Chunks[i];
                    flags = func(files[i].GetExtension());
                    SubInfo.Flags = flags;
                    SubInfo.Offset = (int)bs.Position;
                    if ((flags & DzCompressionFlags.DZ) != 0)
                    {
                        flags &= ~DzCompressionFlags.DZ;
                        flags |= DzCompressionFlags.STORE;
                        SubInfo.Flags = flags;
                        using (BinaryStream bs2 = files[i].OpenAsBinaryStream())
                        {
                            SubInfo.Size = (int)bs2.Length;
                            SubInfo.ZSize_For_Dz = (int)bs2.Length;
                            bs2.StaticCopyTo(bs, buffer);
                        }
                    }
                    else if ((flags & DzCompressionFlags.ZLIB) != 0)
                    {
                        //gzip decompress
                        using (BinaryStream bs3 = new BinaryStream())
                        {
                            using (BinaryStream bs2 = files[i].OpenAsBinaryStream())
                            {
                                SubInfo.Size = (int)bs2.Length;
                                SubInfo.ZSize_For_Dz = (int)bs2.Length;
                                using (GZipStream gZipStream = new GZipStream(bs3, CompressionMode.Compress, true))
                                {
                                    bs2.StaticCopyTo(gZipStream, buffer);
                                }
                            }
                            bs3.Position = 0;
                            bs3.StaticCopyTo(bs, buffer);
                        }
                    }
                    else if ((flags & DzCompressionFlags.BZIP) != 0)
                    {
                        //bzip2 decompress
                        //using (BinaryStream bs3 = new BinaryStream())
                        //{
                        //    using (BinaryStream bs2 = files[i].OpenAsBinaryStream())
                        //    {
                        //        SubInfo.Size = (int)bs2.Length;
                        //        SubInfo.ZSize_For_Dz = (int)bs2.Length;
                        //        using (Ionic.BZip2.BZip2OutputStream bZip2Stream = new Ionic.BZip2.BZip2OutputStream(bs3, true))
                        //        {
                        //            bs2.CopyTo(bZip2Stream);
                        //        }
                        //    }
                        //    bs3.Position = 0;
                        //    bs3.CopyTo(bs);
                        //}
                        throw new NotImplementedException();
                    }
                    else if ((flags & DzCompressionFlags.ZERO) != 0)
                    {
                        //zero chunk
                        using (BinaryStream bs2 = files[i].OpenAsBinaryStream())
                        {
                            SubInfo.Size = (int)bs2.Length;
                            SubInfo.ZSize_For_Dz = 0;
                        }
                    }
                    else if ((flags & DzCompressionFlags.STORE) != 0)
                    {
                        //copy only
                        using (BinaryStream bs2 = files[i].OpenAsBinaryStream())
                        {
                            SubInfo.Size = (int)bs2.Length;
                            SubInfo.ZSize_For_Dz = (int)bs2.Length;
                            bs2.StaticCopyTo(bs, buffer);
                        }
                    }
                    else if ((flags & DzCompressionFlags.LZMA) != 0)
                    {
                        //lzma decompress
                        SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
                        using (BinaryStream bs3 = new BinaryStream())
                        {
                            using (BinaryStream bs2 = files[i].OpenAsBinaryStream())
                            {
                                SubInfo.Size = (int)bs2.Length;
                                SubInfo.ZSize_For_Dz = (int)bs2.Length;
                                coder.WriteCoderProperties(bs3);
                                bs3.WriteInt64(bs2.Length);
                                coder.Code(bs2, bs3, bs2.Length, -1, null);
                            }
                            bs3.Position = 0;
                            bs3.StaticCopyTo(bs, buffer);
                        }
                    }
                    else
                    {
                        //unknow chunk
                        //copy only
                        flags |= DzCompressionFlags.STORE;
                        SubInfo.Flags = flags;
                        using (BinaryStream bs2 = files[i].OpenAsBinaryStream())
                        {
                            SubInfo.Size = (int)bs2.Length;
                            SubInfo.ZSize_For_Dz = (int)bs2.Length;
                            bs2.StaticCopyTo(bs, buffer);
                        }
                    }
                }
                bs.Position = backupOffset;
                dz.WritePart2(bs);
            }
        }
    }
}
