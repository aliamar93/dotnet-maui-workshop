﻿#pragma warning disable CA1416

namespace MonkeyFinder.View;

public partial class MainPage : ContentPageBase
{
    public List<Button> _buttons = new();

	public MainPage(IMonkeyPresenter presenter) : base(presenter)
	{
        InitializeComponent();
        OnInitializeComponent(flexLayout);
    }
}

