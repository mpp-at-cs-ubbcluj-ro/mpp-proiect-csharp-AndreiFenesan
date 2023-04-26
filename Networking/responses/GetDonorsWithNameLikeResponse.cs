using model;

namespace Networking.responses;

[Serializable]
public class GetDonorsWithNameLikeResponse : IResponse
{
    public List<DonorDto> Donors { get; }

    public GetDonorsWithNameLikeResponse(List<DonorDto> donors)
    {
        Donors = donors;
    }
}