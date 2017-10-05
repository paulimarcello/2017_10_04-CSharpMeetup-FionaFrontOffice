namespace FionaFrontoffice_V3.Domain
{
    internal interface ICustomerRepository
    {
        Customer LoadById(CustomerId customerId);

        void SaveCustomer(Customer customer);
    }
}