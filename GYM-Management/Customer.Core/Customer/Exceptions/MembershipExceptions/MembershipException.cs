namespace Customer.Core.Customer.Exceptions.MembershipExceptions;

public class MembershipException:Exception
{
    public MembershipException():base("Start Date Cannot Be Lower Than Current Date")
    {
        
    }
}