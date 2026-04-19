namespace Wasla.Common.Consts;

//public static class DefaultUsers
//{
//    public const string AdminId = "0198e260-1145-79be-a3d9-2e6cbab97464";
//    public const string AdminEmail = "admin@centerforyou.com";
//    public const string AdminPassword = "P@ssword123";
//    public const string AdminSecurityStamp = "3AA96F0F9B1A45D3BE93622F0B4B52E3";
//    public const string AdminConcurrencyStamp = "0198e260114579bea3d92e6d310f8026";
//}


public static class DefaultUsers
{
    public static readonly List<(string Id, string Email, string Password, string SecurityStamp, string ConcurrencyStamp)> Admins =
    [
        (
            "0198e260-1145-79be-a3d9-2e6cbab97464", // guid generator do not remove the dashes
            "admin@wasla.com",
            "P@ssword123",
            "3AA96F0F9B1A45D3BE93622F0B4B52E3",  // tools + create guid 
            "0198e260114579bea3d92e6d310f8026" // online guid generator but remove the dashes
        )
    ];
}
