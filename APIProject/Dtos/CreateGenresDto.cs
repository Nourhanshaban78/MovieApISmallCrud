namespace APIProject.Dtos
{
    public class CreateGenresDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
