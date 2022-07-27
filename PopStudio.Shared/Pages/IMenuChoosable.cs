using System;

namespace PopStudio.Pages
{
    public interface IMenuChoosable
    {
        public string Title { get; set; }

        public Action OnShow { get; }
    }
}
