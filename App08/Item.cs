using SQLite;
using System.Collections.Generic;

namespace App08
{


    [Table("Subitems")]
    public class Subitem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //[Ignore]
        //public List<Item> lista { get; set;}
        [MaxLength(20)]
        public string Name { get; set; }
        public int ParentId { get; set; }
        
        
    }
    [Table("Items")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool ShowImage { get; set; }

        [Ignore]
        public List<Subitem> itemy { get; set; }
    }
}
