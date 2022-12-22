

namespace Namiokai.Models;

public class AuthUser
{
    public string UserName { get; set; }
    public string ImgSource { get; set; }
    public string Email { get; set; }
    public bool Admin { get; set; }

    public AuthUser()
    {

    }

    public AuthUser(string literal)
    {
        var values = literal.Split(';', StringSplitOptions.RemoveEmptyEntries);
        if (values.Length != 4) return;
        
        UserName = values[0];
        ImgSource = values[1];
        Email = values[2];
        Admin = bool.Parse(values[3]);
    }

    public void Clear()
    {
        UserName = string.Empty;
        ImgSource = string.Empty;
        Email = string.Empty;
        Admin = false;
    }

    public override string ToString() => $"{UserName};{ImgSource};{Email};{Admin}";
}
