using SmartPost.Service.DTOs.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;

namespace SmartPost.Desktop.Services;

public class TokenService : IDisposable
{
    private readonly string TOKEN = Path.Combine(Path.GetTempPath(), "40c76fb3-3e9c-4dd1-b592-f53c83c0bd40.txt");
    private readonly string REFRESH_TOKEN = Path.Combine(Path.GetTempPath(), "c4c2e33d-ae51-4c05-9b20-9f9621ecdaa7.txt");
    private readonly string FULL_NAME = Path.Combine(Path.GetTempPath(), "06ead8cf-acc0-428d-a0af-2a1a24e181b7.txt");
    private readonly string PHONE_NUMBER = Path.Combine(Path.GetTempPath(), "d58416e6-4da5-4e78-b030-ddde0f1bd50e.txt");

    public void SaveCreditionals(AuthResultViewModel viewModel)
    {
        Write(FULL_NAME, viewModel.FullName);
        Write(PHONE_NUMBER, viewModel.PhoneNumber);
        Write(TOKEN, viewModel.Token);
        Write(REFRESH_TOKEN, viewModel.RefreshToken);
    }

    public void RemoveCreditionals()
    {
        Write(FULL_NAME, "");
        Write(PHONE_NUMBER, "");
        Write(TOKEN, "");
        Write(REFRESH_TOKEN, "");
    }

    public bool TokenExist()
    {
        FileInfo fileInfo = new FileInfo(TOKEN);
        return fileInfo.Exists && GetToken().Length > 0;
    }

    public string GetToken()
    {
        StreamReader streamReader = new StreamReader(TOKEN);
        var token = streamReader.ReadToEnd();
        streamReader.Close();
        return token;
    }

    public string GetRefreshToken()
    {
        StreamReader streamReader = new StreamReader(REFRESH_TOKEN);
        var token = streamReader.ReadToEnd();
        streamReader.Close();
        return token;
    }

    public string GetFullName()
    {
        StreamReader streamReader = new StreamReader(FULL_NAME);
        var fullname = streamReader.ReadToEnd();
        streamReader.Close();
        return fullname;
    }

    public string GetPhoneNumber()
    {
        StreamReader streamReader = new StreamReader(PHONE_NUMBER);
        var number = streamReader.ReadToEnd();
        streamReader.Close();
        return number;
    }

    public string GetUserId()
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(GetToken());
        var tokenS = jwtSecurityToken as JwtSecurityToken;
        var jti = tokenS.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
        return jti.Value;
    }

    private void Write(string path, string content)
    {
        StreamWriter sw = new StreamWriter(path);
        sw.Write(content);
        sw.Flush();
        sw.Close();
    }

    public void Dispose()
            => GC.SuppressFinalize(this);
}
