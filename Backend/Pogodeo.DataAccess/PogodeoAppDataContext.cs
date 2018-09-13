using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Pogodeo.DataAccess
{
    /// <summary>
    /// This application's database context
    /// </summary>
    public class PogodeoAppDataContext : DbContext
    {
        #region Db Sets

        /// <summary>
        /// Big cities informations table
        /// </summary>
        public DbSet<BigCity> BigCitiesData { get; set; }

        /// <summary>
        /// Small cities association table
        /// </summary>
        public DbSet<SmallCity> SmallCitiesData { get; set; }

        /// <summary>
        /// AccuWeather cached weather table
        /// </summary>
        public DbSet<AccuWeather> AccuWeatherWeather { get; set; }

        /// <summary>
        /// AerisWeather cached weather table
        /// </summary>
        public DbSet<AerisWeather> AerisWeatherWeather { get; set; }

        /// <summary>
        /// WWO (WorldWeatherOnline) cached weather table
        /// </summary>
        public DbSet<WWO> WWOWeather { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PogodeoAppDataContext(DbContextOptions<PogodeoAppDataContext> options) : base(options)
        {
            // If database is not ready for weather usage
            if (CheckInitialDatabaseState()) 
                // Load big cities them
                LoadInitialDatabaseBigCitiesData();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks if current database is in usable state
        /// </summary>
        /// <returns>True if we need new database loading or false if case not</returns>
        public bool CheckInitialDatabaseState()
        {
            // If there are no records
            if (!BigCitiesData.Any()) return true;

            // If there are too few
            if (BigCitiesData.Count() < 78) return true;

            // If there are too many
            if (BigCitiesData.Count() > 78) return true;

            // If there are not enough city names if we select them
            if (BigCitiesData.Select(x => x.CityName).ToList().Count != 78) return true;

            // Otherwise, database is in good state
            return false;
        }

        /// <summary>
        /// Loads the database with initial big cities data that is required to work
        /// </summary>
        public void LoadInitialDatabaseBigCitiesData()
        {
            // Delete any previous entities
            BigCitiesData.RemoveRange(BigCitiesData.ToList());

            // Add our initial database state
            BigCitiesData.Add(new BigCity
            {
                CityName = "Przemyśl",
                Latitude = "49.787",
                Longitude = "22.79",
                AccuWeatherLocalizationKey = "1-275041_1_AL"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Rzeszów",
                Latitude = "50.04",
                Longitude = "22.0",
                AccuWeatherLocalizationKey = "265516"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Krosno",
                Latitude = "49.695",
                Longitude = "21.764",
                AccuWeatherLocalizationKey = "265520"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Jasło",
                Latitude = "49.746",
                Longitude = "21.474",
                AccuWeatherLocalizationKey = "265519"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Ustrzyki Górne",
                Latitude = "49.104",
                Longitude = "22.651",
                AccuWeatherLocalizationKey = "274978"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Kraków",
                Latitude = "50.062",
                Longitude = "19.939",
                AccuWeatherLocalizationKey = "2-274455_1_AL"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Tarnów",
                Latitude = "50.013",
                Longitude = "20.988",
                AccuWeatherLocalizationKey = "274457"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Nowy Sącz",
                Latitude = "49.625",
                Longitude = "20.691",
                AccuWeatherLocalizationKey = "264490"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Gorlice",
                Latitude = "49.658",
                Longitude = "21.159",
                AccuWeatherLocalizationKey = "264480"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Zakopane",
                Latitude = "49.297",
                Longitude = "19.95",
                AccuWeatherLocalizationKey = "264492"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Częstochowa",
                Latitude = "50.808",
                Longitude = "19.127",
                AccuWeatherLocalizationKey = "275785"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Myszków",
                Latitude = "50.574",
                Longitude = "19.323",
                AccuWeatherLocalizationKey = "265965"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Katowice",
                Latitude = "50.26",
                Longitude = "19.013",
                AccuWeatherLocalizationKey = "275781"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Gliwice",
                Latitude = "50.294",
                Longitude = "18.666",
                AccuWeatherLocalizationKey = "275786"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Racibórz",
                Latitude = "50.091",
                Longitude = "18.22",
                AccuWeatherLocalizationKey = "265980"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Bielsko-Biała",
                Latitude = "49.822",
                Longitude = "19.044",
                AccuWeatherLocalizationKey = "275782"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Brzeg",
                Latitude = "50.86",
                Longitude = "17.471",
                AccuWeatherLocalizationKey = "265165"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Opole",
                Latitude = "50.665",
                Longitude = "17.927",
                AccuWeatherLocalizationKey = "274945"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Prudnik",
                Latitude = "50.321",
                Longitude = "17.58",
                AccuWeatherLocalizationKey = "265163"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Namysłów",
                Latitude = "51.076",
                Longitude = "17.717",
                AccuWeatherLocalizationKey = "265185"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Lubin",
                Latitude = "51.399",
                Longitude = "16.203",
                AccuWeatherLocalizationKey = "263016"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Wrocław",
                Latitude = "51.109",
                Longitude = "17.032",
                AccuWeatherLocalizationKey = "273125"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Jelenia Góra",
                Latitude = "50.903",
                Longitude = "15.735",
                AccuWeatherLocalizationKey = "273126"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Kłodzko",
                Latitude = "50.437",
                Longitude = "16.657",
                AccuWeatherLocalizationKey = "263028"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Kielce",
                Latitude = "50.869",
                Longitude = "20.629",
                AccuWeatherLocalizationKey = "275941"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Końskie",
                Latitude = "51.192",
                Longitude = "20.408",
                AccuWeatherLocalizationKey = "266204"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Sandomierz",
                Latitude = "50.677",
                Longitude = "21.749",
                AccuWeatherLocalizationKey = "266205"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Włoszczowa",
                Latitude = "50.853",
                Longitude = "19.967",
                AccuWeatherLocalizationKey = "266233"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Lublin",
                Latitude = "51.246",
                Longitude = "22.568",
                AccuWeatherLocalizationKey = "274231"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Kraśnik",
                Latitude = "50.931",
                Longitude = "22.284",
                AccuWeatherLocalizationKey = "264274"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Zamość",
                Latitude = "50.717",
                Longitude = "23.256",
                AccuWeatherLocalizationKey = "264279"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Chełm",
                Latitude = "51.131",
                Longitude = "23.481",
                AccuWeatherLocalizationKey = "264278"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Międzyrzec Podlaski",
                Latitude = "51.985",
                Longitude = "22.782",
                AccuWeatherLocalizationKey = "264281"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Gorzów Wielkopolski",
                Latitude = "52.73",
                Longitude = "15.238",
                AccuWeatherLocalizationKey = "274270"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Zielona Góra",
                Latitude = "51.939",
                Longitude = "15.507",
                AccuWeatherLocalizationKey = "274269"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Piła",
                Latitude = "53.147",
                Longitude = "16.731",
                AccuWeatherLocalizationKey = "266415"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Poznań",
                Latitude = "52.408",
                Longitude = "16.934",
                AccuWeatherLocalizationKey = "276594"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Śrem",
                Latitude = "52.091",
                Longitude = "17.017",
                AccuWeatherLocalizationKey = "266405"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Konin",
                Latitude = "52.21",
                Longitude = "18.254",
                AccuWeatherLocalizationKey = "266412"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Nowy Tomyśl",
                Latitude = "52.316",
                Longitude = "16.132",
                AccuWeatherLocalizationKey = "266457"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Leszno",
                Latitude = "51.843",
                Longitude = "16.575",
                AccuWeatherLocalizationKey = "266413"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Kalisz",
                Latitude = "51.763",
                Longitude = "18.093",
                AccuWeatherLocalizationKey = "276595"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Łęczyca",
                Latitude = "52.061",
                Longitude = "19.202",
                AccuWeatherLocalizationKey = "268006"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Łowicz",
                Latitude = "52.109",
                Longitude = "19.944",
                AccuWeatherLocalizationKey = "264349"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Łódź",
                Latitude = "51.764",
                Longitude = "19.463",
                AccuWeatherLocalizationKey = "274340"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Wieluń",
                Latitude = "51.22",
                Longitude = "18.57",
                AccuWeatherLocalizationKey = "264354"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Ostrołęka",
                Latitude = "53.081",
                Longitude = "21.573",
                AccuWeatherLocalizationKey = "264802"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Ciechanów",
                Latitude = "52.876",
                Longitude = "20.608",
                AccuWeatherLocalizationKey = "264800"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Płock",
                Latitude = "52.541",
                Longitude = "19.69",
                AccuWeatherLocalizationKey = "264813"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Warszawa",
                Latitude = "50.36",
                Longitude = "21.135",
                AccuWeatherLocalizationKey = "2696858"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Siedlce",
                Latitude = "52.166",
                Longitude = "22.277",
                AccuWeatherLocalizationKey = "264814"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Garwolin",
                Latitude = "51.898",
                Longitude = "21.616",
                AccuWeatherLocalizationKey = "264823"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Radom",
                Latitude = "51.401",
                Longitude = "21.153",
                AccuWeatherLocalizationKey = "274664"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Suwałki",
                Latitude = "54.102",
                Longitude = "22.927",
                AccuWeatherLocalizationKey = "265549"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Białystok",
                Latitude = "53.132",
                Longitude = "23.163",
                AccuWeatherLocalizationKey = "275110"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Łomża",
                Latitude = "53.179",
                Longitude = "22.076",
                AccuWeatherLocalizationKey = "265548"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Siemiatycze",
                Latitude = "52.43",
                Longitude = "22.865",
                AccuWeatherLocalizationKey = "269881"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Elbląg",
                Latitude = "54.156",
                Longitude = "19.4",
                AccuWeatherLocalizationKey = "266368"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Olsztyn",
                Latitude = "53.779",
                Longitude = "20.487",
                AccuWeatherLocalizationKey = "266361"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Ostróda",
                Latitude = "53.7",
                Longitude = "19.961",
                AccuWeatherLocalizationKey = "266366"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Działdowo",
                Latitude = "53.239",
                Longitude = "20.187",
                AccuWeatherLocalizationKey = "266375"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Szczytno",
                Latitude = "53.563",
                Longitude = "20.994",
                AccuWeatherLocalizationKey = "266367"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Giżycko",
                Latitude = "54.037",
                Longitude = "21.768",
                AccuWeatherLocalizationKey = "266363"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Bydgoszcz",
                Latitude = "53.127",
                Longitude = "18.011",
                AccuWeatherLocalizationKey = "273875"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Grudziądz",
                Latitude = "53.484",
                Longitude = "18.756",
                AccuWeatherLocalizationKey = "263631"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Toruń",
                Latitude = "53.011",
                Longitude = "18.612",
                AccuWeatherLocalizationKey = "273877"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Wrocławek",
                Latitude = "53.64",
                Longitude = "19.166",
                AccuWeatherLocalizationKey = "2698736"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Żnin",
                Latitude = "52.847",
                Longitude = "17.728",
                AccuWeatherLocalizationKey = "263669"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Gdańsk",
                Latitude = "54.35",
                Longitude = "18.654",
                AccuWeatherLocalizationKey = "275174"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Hel",
                Latitude = "54.607",
                Longitude = "18.803",
                AccuWeatherLocalizationKey = "265586"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Władysławowo",
                Latitude = "54.79",
                Longitude = "18.409",
                AccuWeatherLocalizationKey = "265611"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Słupsk",
                Latitude = "54.466",
                Longitude = "17.037",
                AccuWeatherLocalizationKey = "275176"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Chojnice",
                Latitude = "53.695",
                Longitude = "17.563",
                AccuWeatherLocalizationKey = "265565"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Szczecin",
                Latitude = "53.433",
                Longitude = "14.548",
                AccuWeatherLocalizationKey = "276655"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Kołobrzeg",
                Latitude = "54.176",
                Longitude = "15.574",
                AccuWeatherLocalizationKey = "266512"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Koszalin",
                Latitude = "54.189",
                Longitude = "16.181",
                AccuWeatherLocalizationKey = "276656"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Szczecinek",
                Latitude = "53.701",
                Longitude = "16.701",
                AccuWeatherLocalizationKey = "266517"
            });
            BigCitiesData.Add(new BigCity
            {
                CityName = "Wałcz",
                Latitude = "53.271",
                Longitude = "16.466",
                AccuWeatherLocalizationKey = "266515"
            });

            //////////////
            SaveChanges();
        }

        #endregion
    }
}
