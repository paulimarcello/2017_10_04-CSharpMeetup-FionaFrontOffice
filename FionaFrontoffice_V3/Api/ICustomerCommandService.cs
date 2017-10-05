using FionaFrontoffice_V3.Api.Command;

namespace FionaFrontoffice_V3.Api
{
    public interface ICustomerCommandService
    {
        void Handle(ApplyNewCustomerCommand command);
        void Handle(AnnounceNewResidenceCommand command);
        void Handle(CancelContractCommand command);
    }
}