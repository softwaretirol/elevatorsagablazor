using Microsoft.JSInterop;

namespace ElevatorSaga;

public class GameJavascriptTranslator
{
    private readonly IElevatorStrategy _elevatorStrategy;
    private readonly IJSRuntime _jsRuntime;
    private readonly string _onSuccessLink;

    public GameJavascriptTranslator(IElevatorStrategy elevatorStrategy, IJSRuntime jsRuntime, string onSuccessLink)
    {
        _elevatorStrategy = elevatorStrategy;
        _jsRuntime = jsRuntime;
        _onSuccessLink = onSuccessLink;
    }

    [JSInvokable("success")]
    public Task<string> Success()
    {
        return Task.FromResult(_onSuccessLink);
    }

    [JSInvokable("init")]
    public async Task Init(int elevatorCount, int floorCount)
    {
        List<FloorJs> floors = new();
        List<ElevatorJs> elevators = new();

        for (int i = 0; i < elevatorCount; i++)
        {
            var elevator = await _jsRuntime.InvokeAsync<IJSObjectReference>("getElevator", i);
            elevators.Add(await ElevatorJs.Create(elevator));
        }

        for (int i = 0; i < floorCount; i++)
        {
            var floor = await _jsRuntime.InvokeAsync<IJSObjectReference>("getFloor", i);
            floors.Add(await FloorJs.Create(floor));
        }

        await _elevatorStrategy.Initialize(floors.AsEnumerable(), elevators.AsEnumerable());
    }
}