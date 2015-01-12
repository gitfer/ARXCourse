namespace MyDocumentalApi.Services
{
    public class MyValueService : IMyValueService
    {
        public string[] GetValues()
        {
            return new[] { "V2aaa", "V2bbb" };
        }
    }
}