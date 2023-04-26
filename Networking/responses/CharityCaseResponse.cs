using model;

namespace Networking.responses;
[Serializable]
public class CharityCaseResponse : IResponse
{
    public List<CharityCaseDto> Cases { get; }

    public CharityCaseResponse(List<CharityCaseDto> cases)
    {
        Cases = cases;
    }
}