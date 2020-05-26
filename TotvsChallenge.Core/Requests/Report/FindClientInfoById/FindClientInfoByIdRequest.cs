using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallenge.Data.Contracts;
using TotvsChallenge.Data.Models;

namespace TotvsChallenge.Core.Requests.Report.FindClientInfoById
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
