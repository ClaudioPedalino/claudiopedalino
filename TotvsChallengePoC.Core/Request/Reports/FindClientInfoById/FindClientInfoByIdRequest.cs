using MediatR;
using TotvsChallengePoC.Data.Models;

namespace TotvsChallengePoC.Core.Request.Reports.FindClientInfoById
{
    public class FindClientInfoByIdRequest : IRequest<ClientInfoModelResponse>
    {
        public FindClientInfoByIdRequest(string clientId)
        {
            ClientId = clientId;
        }

        public string ClientId { get; set; }
    }
}
