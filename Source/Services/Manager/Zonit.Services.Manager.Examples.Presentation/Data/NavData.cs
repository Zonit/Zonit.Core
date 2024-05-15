using Microsoft.Extensions.Hosting;
using Zonit.Extensions.Website;
using Zonit.Extensions.Website.Abstractions.Navigations.Types;

namespace Zonit.Services.Manager.Examples.Presentation.Data;

internal class NavData(INavigationProvider _navigation) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _navigation.Add(new()
        {
            Title = "Dashboard",
            Area = AreaType.Manager,
            Order = 60,
            Icon = IconData.GiftIcon,
            Link = new("Dashboard"),
        });
        _navigation.Add(new()
        {
            Title = "Home",
            Area = AreaType.Manager,
            Order = 60,
            Icon = IconData.GiftIcon,
            Link = new("Home", "Home"),
        });
        _navigation.Add(new()
        {
            Title = "Home2",
            Area = AreaType.Manager,
            Order = 60,
            Icon = IconData.GiftIcon,
            Link = new("Home2", "Home2"),
        });
        _navigation.Add(new()
        {
            Title = "Counter",
            Area = AreaType.Manager,
            Order = 60,
            Icon = IconData.GiftIcon,
            Link = new("Counter", "Counter"),
        });

        _navigation.Add(new()
        {
            Title = "Wallet",
            Area = AreaType.ManagerRight,
            Order = 70,
            Expanded = true,
            Children = [
                new(){
                    Title = "Wallet information",
                    Url = "Wallets",
                    Order = 1,
                    Icon = IconData.InformationIcon
                },
                new(){
                    Title = "Transaction history",
                    Url = "Wallets/Transactions",
                    Order = 2,
                    Icon = IconData.TransactionIcon
                    },
                new()
                {
                    Title = "Recharge history",
                    Url = "Wallets/Recharge",
                    Order = 3,
                    Icon = IconData.RechargeIcon
                    }
            ]
        }); 
        
        _navigation.Add(new()
        {
            Title = "Wallet",
            Area = AreaType.Manager,
            Order = 70,
            Expanded = true,
            Icon = IconData.RechargeIcon,
            Children = [
                new(){
                    Title = "Wallet information",
                    Url = "Wallets",
                    Order = 1,
                    Icon = IconData.InformationIcon
                },
                new(){
                    Title = "Transaction history",
                    Url = "Wallets/Transactions",
                    Order = 2,
                    Icon = IconData.TransactionIcon
                    },
                new()
                {
                    Title = "Recharge history",
                    Url = "Wallets/Recharge",
                    Order = 3,
                    Icon = IconData.RechargeIcon
                    }
            ]
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}