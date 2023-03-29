using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System.Net;
using JasperReports.ReportExecutions;
using JasperReports.Resources;

namespace JasperReport.Api.Client
{
    public class JasperReportClient
    {
        private string _hostUrl;
        private HttpClient _httpClient;

        private RefitSettings _refitSettings;
        private HttpMessageHandler _httpMessageHandler;
        internal static JsonSerializerSettings JsonSerializerSettings { get; private set; }
        internal static IHttpContentSerializer JsonContentSerializer { get; private set; }
        private Lazy<IResourceService> _resourceService;
        private Lazy<IReportExecutionsService> _reportExecutionsService;

        static JasperReportClient()
        {
            JsonSerializerSettings = JsonSerializerSettings ?? new JsonSerializerSettings
            {
                ContractResolver = new CustomCamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore, // do not send empty fields
            };

            JsonSerializerSettings.Converters.Add(new StringEnumConverter());
            JsonSerializerSettings.Converters.Add(new CustomIsoDateTimeConverter());

            JsonContentSerializer = JsonContentSerializer ?? new NewtonsoftJsonContentSerializer(JsonSerializerSettings);
        }

        private class CustomIsoDateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
        {
            public CustomIsoDateTimeConverter()
            {
                Culture = CultureInfo.InvariantCulture;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is DateTime dateTime)
                {
                    if ((DateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                        || (DateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
                    {
                        dateTime = dateTime.ToUniversalTime();
                    }

                    writer.WriteValue(dateTime.ToJavaISO8601());
                }
                else
                {
                    base.WriteJson(writer, value, serializer);
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                bool nullable = objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);

                if (reader.TokenType == JsonToken.String)
                {
                    string dateText = reader.Value.ToString();

                    if (string.IsNullOrEmpty(dateText) && nullable)
                        return null;

                    if (dateText.EndsWith("UTC"))
                    {
                        if (DateTime.TryParseExact(dateText.Replace("UTC", "Z"), "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFK", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var dt))
                            return dt;
                    }
                }

                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        private void Initialize()
        {
            _httpMessageHandler = _httpMessageHandler ?? new ErrorMessageHandler();

            _refitSettings = _refitSettings ?? new RefitSettings
            {
                ContentSerializer = JsonContentSerializer,
                UrlParameterFormatter = new CustomUrlParameterFormatter(),
                HttpMessageHandlerFactory = () => _httpMessageHandler
            };
        }

        private class CustomUrlParameterFormatter : DefaultUrlParameterFormatter
        {
            public override string Format(object parameterValue, ICustomAttributeProvider attributeProvider, Type type)
            {
                if (parameterValue is bool)
                    return string.Format(CultureInfo.InvariantCulture, "{0}", parameterValue).ToLowerInvariant();

                return base.Format(parameterValue, attributeProvider, type);
            }
        }

        private class CustomCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
        {
            // preserve exact dictionary key
            protected override string ResolveDictionaryKey(string dictionaryKey) => dictionaryKey;
        }

        private JasperReportClient(string hostUrl)
        {
            _hostUrl = hostUrl;
            Initialize();
            CreateServices();
        }

        private JasperReportClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Initialize();
            CreateServices();
        }

        private JasperReportClient(string hostUrl, string user, string password)
        {
            _hostUrl = hostUrl;
            var client = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, hostUrl + "/rest_v2/login");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("j_username", user));
            collection.Add(new("j_password", password));
            httpRequestMessage.Content = new FormUrlEncodedContent(collection);
            var response = client.SendAsync(httpRequestMessage).Result;
            // response.EnsureSuccessStatusCode();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                client = new HttpClient(); // 請求時需要jsessionId
                foreach (string item in response.Headers.GetValues("Set-Cookie"))
                {
                    client.DefaultRequestHeaders.Add("Cookie", item);
                }
                client.BaseAddress = new Uri(hostUrl);
                _httpClient = client;
            }
            else
            {
                throw new Exception("login fail.");
            }
            Initialize();
            CreateServices();
        }

        private JasperReportClient(string hostUrl, HttpMessageHandler httpMessageHandler)
        {
            _hostUrl = hostUrl;
            _httpMessageHandler = httpMessageHandler;
            Initialize();
            CreateServices();
        }

        private Lazy<I> CreateService<I>()
        {
            if (_httpClient != null)
                return new Lazy<I>(() => RestService.For<I>(_httpClient, _refitSettings));
            else
                return new Lazy<I>(() => RestService.For<I>(_hostUrl, _refitSettings));
        }

        // public static JasperReportClient Create(string hostUrl)
        // {
        //     return new JasperReportClient(hostUrl);
        // }
        public static JasperReportClient Create(string hostUrl, string user, string password)
        {
            return new JasperReportClient(hostUrl, user, password);
        }

        // public static JasperReportClient Create(string hostUrl, HttpMessageHandler httpMessageHandler)
        // {
        //     return new JasperReportClient(hostUrl, httpMessageHandler);
        // }

        // public static JasperReportClient Create(HttpClient httpClient)
        // {
        //     return new JasperReportClient(httpClient);
        // }

        /* api註冊 */
        private void CreateServices()
        {
            _reportExecutionsService = CreateService<IReportExecutionsService>();
            _resourceService = CreateService<IResourceService>();
        }

        public ReportExecutionsService ReportExecutions => new ReportExecutionsService(_reportExecutionsService.Value);
        public ResourceService Resource => new ResourceService(_resourceService.Value);
    }
}
