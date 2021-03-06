﻿using System;

namespace Mine.Models
{
    /// <summary>
    /// Items for characters and monsters to use.
    /// </summary>
    public class ItemModel
    {
        //The ID for the item
        public string Id { get; set; }

        //The Display Text for the item
        public string Text { get; set; }

        //The Description of the item
        public string Description { get; set; }

        //The Value of the Item Damage
        public int Value { get; set; } = 0;
    }
}