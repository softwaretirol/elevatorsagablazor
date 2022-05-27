namespace ElevatorSagaBlazor;

public interface IGameImplementation
{
    Task Initialize(IEnumerable<IFloor> floors, IEnumerable<IElevator> elevator);
}