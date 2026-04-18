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
            "admin@centerforyou.com",
            "P@ssword123",
            "3AA96F0F9B1A45D3BE93622F0B4B52E3",  // tools + create guid 
            "0198e260114579bea3d92e6d310f8026" // online guid generator but remove the dashes
        ),
        (
            "6690d2ca-ae26-450d-8eff-db90199265b3",
            "admin2@centerforyou.com",
            "EMAMemam@123",
            "E369C6740AD64DF195060E41FCC00BB4",
            "ae6f90d2f7f143b5957e1d396ac6dd0a"
        ),
        (
            "838a343e-dcc2-4198-ae43-1feed65b567f",
            "admin3@centerforyou.com",
            "P@ssword123",
            "0576E3E394064D39A2AC324250B7DD44",
            "684197596a464981bfb971b0a8f45064"
        ),
    ];
}
