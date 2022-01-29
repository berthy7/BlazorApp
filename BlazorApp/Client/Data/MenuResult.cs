namespace BlazorApp.Client.Data
{
    public class MenuResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public Menu[] menu { get; set; }
    }
}
