using ElevatorSaga;

namespace ElevatorSagaBlazor;

public class ElevatorStrategy : IElevatorStrategy
{
    public async Task Initialize(IEnumerable<IFloor> floors, IEnumerable<IElevator> elevators)
    {
        foreach (var floor in floors)
        {
            var number = await floor.GetFloorNumber();
        }

        foreach (var elevator in elevators)
        {
        }
    }
}