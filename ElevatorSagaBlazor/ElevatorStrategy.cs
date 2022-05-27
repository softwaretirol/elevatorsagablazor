using ElevatorSaga;

namespace ElevatorSagaBlazor
{
    public class ElevatorStrategy : IElevatorStrategy
    {
        public async Task Initialize(IEnumerable<IFloor> floors, IEnumerable<IElevator> elevators)
        {
            foreach (var floor in floors)
            {
                var number = await floor.GetFloorNumber();
                floor.UpButtonPressed += MoveToFloor;
                floor.DownButtonPressed += MoveToFloor;

                void MoveToFloor()
                {
                    elevators.First().GoToFloor(number);
                }
            }

            foreach (var elevator in elevators)
            {
                elevator.FloorButtonPressed += x => elevator.GoToFloor(x);
            }
        }
    }
}
