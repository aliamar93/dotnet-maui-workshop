﻿#pragma warning disable CA1416

using Adventures.Commands;
using Adventures.Common;
using Adventures.Common.Interfaces;
using Adventures.ViewModel;
using MonkeyFinder.Commands;
using MonkeyFinder.Presenters;
using MonkeyFinder.Services;
using MonkeyFinder.View;

namespace MonkeyFinder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

    	builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<IPresenter, MonkeyPresenter>();

		builder.Services.AddSingleton<IListViewModel,ListViewModel>();
		builder.Services.AddSingleton<IDetailViewModel,DetailsViewModel>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<DetailsPage>();

		// Shared commands
		builder.Services.AddSingleton<IMvpCommand, MessageCommand>();
		builder.Services.AddSingleton<IMvpCommand, ClosestItemCommand>();
		builder.Services.AddSingleton<IMvpCommand, ShowonMapCommand>();

		// Specific to Monkey finder
		builder.Services.AddSingleton<IMvpCommand, GotoToSelectedMonkeyCommand>();
		builder.Services.AddSingleton<IMvpCommand, GetMonkeyListCommand>();

		builder.Services.AddSingleton<IDataService>(provider =>
		{
			IConnectivity connectivity = provider
				.GetServices<IConnectivity>().FirstOrDefault();

			return connectivity.NetworkAccess != NetworkAccess.Internet
			  ? new MonkeyOfflineService()
			  : new MonkeyOnlineService();
		});

		var serviceBuilder =  builder.Build();

		return serviceBuilder;

	}
}
