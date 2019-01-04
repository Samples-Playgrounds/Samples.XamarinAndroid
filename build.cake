//#addin Cake.Curl
#tool nuget:?package=sharpcompress&version=0.22.0

var TARGET = Argument ("t", Argument ("target", "Default"));

Dictionary<string, string> Externals = new Dictionary<string, string>
{
	{
		"BottomNavigationView-hitherejoe",
		"https://github.com/hitherejoe/BottomNavigationViewSample/archive/master.zip"
	},
	{
		"BottomNavigationView-vipulasri",
		"https://github.com/vipulasri/Bottom-Navigation-View-Sample/archive/master.zip"
	},
	{
		"BottomNavigationView-Truiton",
		"https://github.com/Truiton/BottomNavigation/archive/master.zip"
	},
	{
		"BottomNavigationView-NealRDC",
		"https://github.com/NealRDC/Android-BottomNavigationView-example/archive/master.zip"
	},
	{
		"CoordinatorLayout-saulmm",
		"https://github.com/saulmm/CoordinatorExamples/archive/master.zip"
	},
	{
		"CoordinatorLayout-loonggg",
		"https://github.com/loonggg/CoordinatorLayoutDemo/archive/master.zip"
	},
	{
		"CoordinatorLayout-gdutxiaoxu",
		"https://github.com/gdutxiaoxu/CoordinatorLayoutExample/archive/master.zip"
	},
	{
		"CoordinatorLayout-sungerk",
		"https://github.com/sungerk/CoordinatorLayoutDemos/archive/master.zip"
	},
	{
		"CoordinatorLayout-ggajews",
		"https://github.com/ggajews/coordinatorlayoutwithfabdemo/archive/master.zip"
	},
	{
		"CoordinatorLayout-takahirom",
		"https://github.com/takahirom/webview-in-coordinatorlayout/archive/master.zip"
	},
	{
		"CoordinatorLayout-zhaochenpu",
		"https://github.com/zhaochenpu/CoordinatorLayoutDemo/archive/master.zip"
	},
	{
		"CoordinatorLayout-HeroBarry",
		"https://github.com/HeroBarry/CoordinatorLayout/archive/master.zip"
	},
	{
		"CoordinatorLayout-ttymsd",
		"https://github.com/ttymsd/coordinator-behaviors/archive/master.zip"
	},
	{
		"CoordinatorLayout-romainz",
		"https://github.com/romainz/CoordinatorLayoutSample/archive/master.zip"
	},
	{
		"CoordinatorLayout-cstew",
		"https://github.com/cstew/CustomBehavior/archive/master.zip"
	},
	{
		"CoordinatorLayout-vitovalov",
		"https://github.com/vitovalov/TabbedCoordinatorLayout/archive/master.zip"
	},

	// {
	// 	"-master",
	// 	""
	// },
};

Task("externals")
	.Does
	(
		() =>
		{
			EnsureDirectoryExists("./externals/");
			foreach(KeyValuePair<string, string> url in Externals)
			{
				if (!FileExists($"./externals/{url.Key}"))
				{
					Information ($"DownloadFile(${url.Value}.zip, ./externals/{url.Key});");
					DownloadFile($"{url.Value}", $"./externals/{url.Key}.zip");

					// CurlDownloadFile
					// 	(
					// 		new Uri(url.Value),
					// 		new CurlDownloadSettings
					// 		{
					// 			OutputPaths = new FilePath[] { $"./externals/{url.Key}" }
					// 		}
					// 	);
				}
				try
				{
					Information ($"Unzip (./externals/{url.Key}.zip, ./externals/);");
					Unzip ($"./externals/{url.Key}.zip", $"./externals/{url.Key}");
				}
				catch (System.Exception exc)
				{
					Error ($"exception : {exc?.Message}");
				}
			}
		}
	);

RunTarget ("externals");

