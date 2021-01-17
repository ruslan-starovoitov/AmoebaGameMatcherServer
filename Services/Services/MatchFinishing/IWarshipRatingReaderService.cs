using System.Threading.Tasks;

namespace Services.Services.MatchFinishing
{
    public interface IWarshipRatingReaderService
    {
        Task<int> ReadWarshipRatingAsync(int warshipId);
    }
}