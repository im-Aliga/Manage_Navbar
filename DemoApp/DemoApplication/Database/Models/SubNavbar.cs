using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class SubNavbar:BaseEntity
    {   
        public string Title { get; set; }

        public int Order { get; set; }

        public string Url { get; set; }

        public int NavbarId { get; set; }   
        public NavbarM Navbar { get; set; }


    }
}
