using System.Text.Json;
using antheap1.Models;

namespace antheap1.Services;

public static class RejestrWL
{
    public static Organization? FindOrganizationByNIP(string nip, ref string? status)
    {
        // string host = "https://wl-test.mf.gov.pl/api/";
        string host = "https://wl-api.mf.gov.pl/api/";
        Organization? item = null;

        using (var client = new HttpClient())
        {
            string data = DateTime.Now.ToString("yyyy-MM-dd");
            string url = host + "search/nip/" + nip + "?date=" + data;

            HttpResponseMessage response = client.Send(new HttpRequestMessage(HttpMethod.Get, url));
            if (response.IsSuccessStatusCode)
            {
                // var contentStream = @"{""result"":{""subject"":{""name"":""\""INTELLINET\"" SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ"",""nip"":""6310100408"",""statusVat"":""Czynny"",""regon"":""008433514"",""pesel"":null,""krs"":""0000116574"",""residenceAddress"":null,""workingAddress"":""KS. BPA HERBERTA BEDNORZA 2A-6, 40-386 KATOWICE"",""representatives"":[],""authorizedClerks"":[],""partners"":[],""registrationLegalDate"":""2002-07-01"",""registrationDenialBasis"":null,""registrationDenialDate"":null,""restorationBasis"":null,""restorationDate"":null,""removalBasis"":null,""removalDate"":null,""accountNumbers"":[""87109020080000000100556488""],""hasVirtualAccounts"":false},""requestId"":""onUrQ-8g41dne"",""requestDateTime"":""11-07-2022 23:26:14""}}";
                var contentStream = response.Content.ReadAsStream();
                try
                {
                    JsonDocument json = JsonDocument.Parse(contentStream);
                    JsonElement res = json.RootElement.GetProperty("result").GetProperty("subject");
                    var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                    item = JsonSerializer.Deserialize<Organization>(res, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
                }
                catch (Exception e)
                {
                    status = $"Odebrano błędne dane ({e.Message})";
                }
            }
            else
            {
                status = $"Błąd połączenia z serwerem {host} ({response.StatusCode.ToString()})";         
            }
        }
        return item;
    }
}
