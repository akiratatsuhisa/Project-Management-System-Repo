namespace WebApplication.Models
{
    public class ProjectStatusViewModel
    {
        public string Name { get; set; }

        public string TextClassName { get; set; }

        public override string ToString() => Name;
    }
}