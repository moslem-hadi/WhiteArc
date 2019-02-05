﻿using System.Collections.Generic;

namespace ViewModels
{
    public class PagedListViewModel<T>
    {
        public IList<T> List { get; set; }
        public int TotalCount { get; set; }
    }
}
