using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public class ViewListNinja
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ServedInOniwaban { get; set; }
        public string ClanName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}