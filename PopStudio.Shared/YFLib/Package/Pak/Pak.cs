using System.Xml;
using PopStudio.Plugin;
using System;
using System.Collections.Generic;
using static PopStudio.PlatformAPI.YFFileSystem;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace PopStudio.Package.Pak
{
    /// <summary>
    /// SexyApp Framework Pak 
    /// </summary>
    internal static class Pak
    {
        public static void Pack(YFDirectory inFolder, YFFile outFile, Func<string, PakCompressionFlags> func)
        {
            byte[] buffer = new byte[81920];
            bool TVPak = false;
            PakInfo pak = new PakInfo
            {
                fileInfoLibrary = new List<FileInfo>()
            };
            //read some parameter
            YFFile xmlpath = inFolder.GetYFDirectory("popstudioinfo")?.GetYFFile("packinfo.xml");
            if (xmlpath is not null)
            {
                string xmldata;
                using (Stream stream = xmlpath.OpenAsStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        xmldata = sr.ReadToEnd();
                    }
                }
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmldata);
                var root = xml.SelectSingleNode("/PackInfo");
                if (root == null) return;
                var childlist = root.ChildNodes;
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "PCVersion")
                    {
                        pak.pc = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "TVVersion")
                    {
                        TVPak = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "WindowsPathSeparate")
                    {
                        pak.win = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "Xbox360PtxAlign")
                    {
                        pak.x360 = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "XmemCompress")
                    {
                        pak.xmem = Convert.ToBoolean(child.InnerText);
                    }
                    else if (child.Name == "ZlibCompress")
                    {
                        pak.compress = Convert.ToBoolean(child.InnerText);
                    }
                }
            }
            else
            {
                pak.pc = true;
                pak.win = true;
                pak.x360 = false;
                pak.xmem = false;
                pak.compress = false;
            }
            if (TVPak)
            {
                throw new NotImplementedException();
                //return;
            }
            YFFile[] a = inFolder.GetAllFilesWithSubDirectoryFiles();
            int temp = inFolder.Name.Length + 2;
            using TempFilePool tempFilePool = new TempFilePool();
            using (BinaryStream bs_files = pak.pc
                ? new BinaryStream(new FileStream(tempFilePool.Add(), FileMode.Create))
                : outFile.CreateAsBinaryStream())
            {
                bs_files.Encode = EncodeHelper.ANSI;
                //write head
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == xmlpath) continue;
                    FileInfo info = new FileInfo();
                    pak.fileInfoLibrary.Add(info);
                    if (pak.win)
                    {
                        info.fileName = a[i].GetPath(inFolder)[temp..].Replace('/', '\\');
                    }
                    else
                    {
                        info.fileName = a[i].GetPath(inFolder)[temp..].Replace('\\', '/');
                    }
                }
                pak.Write(bs_files);
                int jian = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == xmlpath)
                    {
                        jian++;
                        continue;
                    }
                    if (!pak.pc)
                    {
                        if (pak.x360 && a[i].GetExtension().ToLower() == ".ptx")
                        {
                            PakInfo.Fill0x1000(bs_files);
                        }
                        else
                        {
                            PakInfo.Fill(bs_files);
                        }
                    }
                    FileInfo info = pak.fileInfoLibrary[i - jian];
                    using (BinaryStream bs_thisfile = a[i].OpenAsBinaryStream())
                    {
                        if (pak.compress == true)
                        {
                            PakCompressionFlags method = func(a[i].GetExtension());
                            switch (method)
                            {
                                case PakCompressionFlags.STORE:
                                    bs_thisfile.StaticCopyTo(bs_files, buffer);
                                    info.zsize = (int)bs_thisfile.Length;
                                    break;
                                case PakCompressionFlags.ZLIB:
                                    using (BinaryStream bs3 = new BinaryStream())
                                    {
                                        using (ZLibStream zLibStream = new ZLibStream(bs3, CompressionMode.Compress, true))
                                        {
                                            bs_thisfile.StaticCopyTo(zLibStream, buffer);
                                        }
                                        bs3.Position = 0;
                                        bs3.StaticCopyTo(bs_files, buffer);
                                        info.size = (int)bs_thisfile.Length;
                                        info.zsize = (int)bs3.Length;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            bs_thisfile.StaticCopyTo(bs_files, buffer);
                            info.zsize = (int)bs_thisfile.Length;
                        }
                    }
                }
                bs_files.Position = 0;
                pak.Write(bs_files);
                if (pak.pc)
                {
                    using (BinaryStream bs = outFile.CreateAsBinaryStream())
                    {
                        int endlength = (int)bs_files.Length;
                        bs_files.Position = 0;
                        for (int i = 0; i < endlength; i++)
                        {
                            bs.WriteUInt8((byte)(bs_files.ReadUInt8() ^ 0xF7));
                        }
                    }
                }
                else
                {
                    if (pak.xmem) throw new NotImplementedException();
                }
            }
        }

        public static void Unpack(YFFile inFile, YFDirectory outFolder, bool changeimage, bool delete)
        {
            byte[] buffer = new byte[81920];
            List<Task> decodeImage = null;
            if (changeimage)
            {
                decodeImage = new List<Task>();
            }
            YFFile tempName;
            using (BinaryStream bs = new BinaryStream())
            {
                bs.Encode = EncodeHelper.ANSI;
                bool TVMode = false;
                PakInfo pak = new PakInfo();
                using (BinaryStream bs_origin = inFile.OpenAsBinaryStream())
                {
                    int magic = bs_origin.PeekInt32();
                    if (magic == 1295498551) //37 BD 37 4D
                    {
                        //pak PC xor 0xf7 encrypt
                        pak.pc = true;
                        //pak.compress = false;
                        int l = (int)bs_origin.Length;
                        for (int i = 0; i < l; i++)
                        {
                            bs.WriteUInt8((byte)(bs_origin.ReadUInt8() ^ 0xF7));
                        }
                    }
                    else if (magic == -1161803072) //C0 4A C0 BA
                    {
                        //pak game console
                        bs_origin.StaticCopyTo(bs, buffer);
                        //bs.WriteBytes(bs_origin.ReadBytes((int)bs_origin.Length));
                    }
                    else if (magic == -317524721) //0F F5 12 ED
                    {
                        //xmem compress file in xbox360
                        //Invalid
                        bs_origin.Endian = Endian.Big;
                        pak.xmem = true;
                        pak.compress = false;
                        throw new NotImplementedException();
                    }
                    else if (magic == 67324752) //TV(zip)50 4B 03 04
                    {
                        TVMode = true;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                bs.Position = 0;
                if (TVMode)
                {
                    throw new NotImplementedException(); //in .net standard it can't work well in Android when decompress a zip file from Windows
                }
                else
                {
                    pak.Read(bs);
                    if (pak.fileInfoLibrary == null) return;
                    string adds = outFolder.Name + "/";
                    for (int i = 0; i < pak.fileInfoLibrary.Count; i++)
                    {
                        if (!pak.pc) pak.x360 |= PakInfo.Jump(bs);
                        string newname = adds + pak.fileInfoLibrary[i].fileName;
                        newname = newname.Replace('\\', '/').Replace("//", "/");
                        tempName = CreateYFFileFromPath(newname, outFolder);
                        byte firstbyte = 0;
                        if (pak.fileInfoLibrary[i].size != 0)
                        {
                            using (BinaryStream bs2 = new BinaryStream())
                            {
                                bs.StaticCopyTo(bs2, pak.fileInfoLibrary[i].zsize, buffer);
                                bs2.Position = 0;
                                using (ZLibStream zLibStream = new ZLibStream(bs2, CompressionMode.Decompress))
                                {
                                    using (BinaryStream bs3 = tempName.CreateAsBinaryStream())
                                    {
                                        zLibStream.StaticCopyTo(bs3, buffer);
                                        bs3.Position = 0;
                                        firstbyte = bs3.ReadByte();
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (BinaryStream bs2 = tempName.CreateAsBinaryStream())
                            {
                                bs.StaticCopyTo(bs2, pak.fileInfoLibrary[i].zsize, buffer);
                                bs2.Position = 0;
                                firstbyte = bs2.ReadByte();
                            }
                        }
                        if (changeimage && tempName.GetExtension().ToLower() == ".ptx")
                        {
                            if (pak.x360)
                            {
                                decodeImage.Add(Task.Run(() =>
                                {
                                    YFAPI.DecodeImage(tempName, tempName.Parent.CreateYFFile(tempName.GetNameWithoutExtension() + ".png"), 6);
                                    if (delete)
                                    {
                                        tempName.DeleteSelf();
                                    }
                                }));
                            }
                            else
                            {
                                int image_format = -1;
                                if (firstbyte == 0x44)
                                {
                                    image_format = 5;
                                }
                                else if (firstbyte == 0x47)
                                {
                                    image_format = 7;
                                }
                                if (image_format != -1)
                                {
                                    decodeImage.Add(Task.Run(() =>
                                    {
                                        YFAPI.DecodeImage(tempName, tempName.Parent.CreateYFFile(tempName.GetNameWithoutExtension() + ".png"), image_format);
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
                YFFile lst = outFolder.CreateYFDirectory("popstudioinfo").CreateYFFile("packinfo.xml");
                using (Stream stream = lst.OpenAsStream())
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        sw.WriteLine("<!-- DO NOT EDIT THIS FILE. This file is generated by PopStudio. (unless you want to change the way for pack) -->");
                        sw.WriteLine("<PackInfo version=\"1\">");
                        sw.WriteLine("    <PCVersion>" + pak.pc + "</PCVersion>");
                        sw.WriteLine("    <TVVersion>" + TVMode + "</TVVersion>");
                        sw.WriteLine("    <WindowsPathSeparate>" + pak.win + "</WindowsPathSeparate>");
                        sw.WriteLine("    <Xbox360PtxAlign>" + pak.x360 + "</Xbox360PtxAlign>");
                        sw.WriteLine("    <XmemCompress>" + pak.xmem + "</XmemCompress>");
                        sw.WriteLine("    <ZlibCompress>" + (pak.compress == true) + "</ZlibCompress>");
                        sw.WriteLine("</PackInfo>");
                    }
                }
                if (changeimage && decodeImage is not null)
                {
                    Task.WaitAll(decodeImage.ToArray());
                }
            }
        }
    }
}
