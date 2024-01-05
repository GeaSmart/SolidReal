namespace MySolidAPI.Dtos
{
    public class TareaConUsuarioDto
    {
        public TareaConUsuarioDto(int id, string userName, string title)
        {
            Id = id;
            UserName = userName;
            Title = title;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
    }
}
