namespace ElevatorSaga;

public interface IElevatorStrategy
{
    Task Initialize(IEnumerable<IFloor> floors, IEnumerable<IElevator> elevator);
}