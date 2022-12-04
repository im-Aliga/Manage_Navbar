using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class NavbarM :BaseEntity
    {   

        public string Title { get; set; }   

        public int Order { get; set; }
        public string Url { get; set; }
        public bool IsBold { get; set; }
        
        public bool IsShowHeader { get; set; }
        public bool IsShowFooter { get; set; }

        public List<SubNavbar> SubNavbars { get; set; }



        
        
    }
}
