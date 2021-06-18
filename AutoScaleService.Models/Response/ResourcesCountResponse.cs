namespace AutoScaleService.Models.Response
{
    public class ResourcesCountResponse
    {
        public ResourcesCountResponse(int count)
        {
            Count = count;
        }

        public int Count { get; set; }
    }
}
