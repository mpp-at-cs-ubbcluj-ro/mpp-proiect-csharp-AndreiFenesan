namespace Networking.requests;
[Serializable]
public class GetDonorWithNameLikeRequest : IRequest
{
    public string NameContains { get; set; }

    public GetDonorWithNameLikeRequest(string nameContains)
    {
        NameContains = nameContains;
    }
    
}