using System.ComponentModel;

namespace AsanPardakht.Core.Enums;

public enum ErrorCode : byte
{
    [Description("مقدار استان نامعتبر است")]
    ProvinceIsInvalid = 100,

    [Description("مقدار شهر نامعتبر است")]
    CityIsInvalid = 101,

    [Description("مقدار آدرس نامعتبر است")]
    AddressIsInvalid = 102,

    [Description("مقدار قبلا ثبت شده است")]
    LocationIsDuplicated = 103,

    [Description("آدرس مورد نظر یافت نشد")]
    LocationNotFound = 104,

    [Description("شناسه کاربر نادرست است")]
    UserIdIsInvalid = 105,
}