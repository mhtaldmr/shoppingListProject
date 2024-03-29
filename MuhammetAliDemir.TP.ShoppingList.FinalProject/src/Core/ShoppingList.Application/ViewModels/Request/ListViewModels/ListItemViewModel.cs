﻿namespace ShoppingList.Application.ViewModels.Request.ListViewModels
{
    public class ListItemUpdateViewModel : ListItemViewModel
    {
        public int Id { get; set; }
    }

    public class ListItemViewModel : ListItemCreateViewModel
    {
        public bool IsChecked { get; set; } = false;
    }

    public class ListItemCreateViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; } = 0;
        public int UoMId { get; set; } = 1;
    }

    public class ListItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Quantity { get; set; }
        public string UoMCode { get; set; }
    }
}