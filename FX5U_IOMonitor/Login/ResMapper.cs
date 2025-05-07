using System.Text.Json;
using System.Text.Json.Serialization;

namespace FX5U_IOMonitor.Login
{
	#region Language Data Structure

	public class Language
	{
		[JsonPropertyName( "Name" )]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName( "Code" )]
		public string Code { get; set; } = string.Empty;
	}

	public class LanguageData
	{
		[JsonPropertyName( "Language" )]
		public List<Language> Languages { get; set; } = new();

		[JsonExtensionData]
		public Dictionary<string, JsonElement> Strings { get; set; } = new();
	}

	#endregion

	internal static class ResMapper
	{
		internal static readonly string LanguageFilePath = Path.Combine( Environment.CurrentDirectory, "Ref" );

		static LanguageData? _languageData;
		internal static List<Language> Languages => _languageData?.Languages ?? new();
		static Dictionary<string, string>? _strings;

		internal static bool LoadLocalizedStrings( string cultureCode )
		{
			var filePath = Path.Combine( LanguageFilePath, $"strings.{cultureCode}.json" );
			LanguageData? languageData = null;
			if( File.Exists( filePath ) ) {
				var json = File.ReadAllText( filePath );
				try {
					languageData = JsonSerializer.Deserialize<LanguageData>( json );
				}
				catch( Exception ex ) {
					MessageBox.Show( $"Failed to load language data: {ex.Message}" );
					return false;
				}
			}
			else {
				MessageBox.Show( "Target language file can not found" );
				return false;
			}

			if( languageData == null ) {
				MessageBox.Show( "Failed to load language data" );
				return false;
			}

			// Update language data
			_languageData = languageData;
			_strings = new();
			foreach( var item in _languageData.Strings ) {
				_strings.Add( item.Key, item.Value.GetString() );
			}
			return true;
		}

		internal static string GetLocalizedString( string key )
		{
			if( _strings == null || _strings.Count() == 0 ) {
				return key;
			}

			return _strings.ContainsKey( key ) ? _strings[ key ] : key;
		}
	}
}
