using AutoMapper;
using PurchaseApi.Communication.Request;
using PurchaseApi.Communication.Response;
using PurchaseApi.Domain.entities;

namespace PurchaseApi.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestEntity();
            EntityToResponse();
        }
        private void RequestEntity()
        {
            CreateMap<RequestPurchaseItem, Purchase>();
        }
        private void EntityToResponse()
        {
            CreateMap<Purchase, ResponsePurchase>();
        }
    }
}
