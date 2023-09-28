namespace PhotoProjectAPI.DTO
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? User { get; set; }
        public string UserId { get; set; }
        public List<PhotoDTO> Photos { get; set; }
    }
}
