namespace Nyala.Core
{
    public class Rover : IRover
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public DirectionEnum Direction { get; set; }
        public int GridSize { get; set; }
        public List<(int, int)> Obtacles { get; set; }

        public Rover(int gridSize) : this(0, 0, DirectionEnum.North)
        {
            this.GridSize = gridSize;
        }

        public Rover(int xCoordinate, int yCoordinate, DirectionEnum direction)
        {
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
            this.Direction = direction;
            this.Obtacles = new List<(int, int)>();
        }

        public string GetCurrentCoordinates()
        {
            return $"{this.XCoordinate},{this.YCoordinate} facing {this.Direction}";
        }

        public void Command(string setOfCommands)
        {
            var commands = setOfCommands.Split(",").AsEnumerable();
            foreach (var command in commands)
            {
                switch (command)
                {
                    case "f":
                        this.MoveForward();
                        break;
                    case "b":
                        this.MoveBackward();
                        break;
                    case "l":
                        this.MoveLeft();
                        break;
                    case "r":
                        this.MoveRight();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveForward()
        {
            var newY = this.YCoordinate < this.GridSize ? this.YCoordinate + 1 : 0;
            this.DetectObstacles((this.XCoordinate, newY));
            this.YCoordinate = newY;
            this.Direction = DirectionEnum.North;
        }

        private void MoveBackward()
        {
            var newY = this.YCoordinate == 0 ? this.GridSize : this.YCoordinate - 1;
            this.DetectObstacles((this.XCoordinate, newY));
            this.YCoordinate = newY;
            this.Direction = DirectionEnum.South;
        }

        private void MoveRight()
        {
            var newX = this.XCoordinate < this.GridSize ? this.XCoordinate + 1 : 0;
            this.DetectObstacles((newX, this.YCoordinate));
            this.XCoordinate = newX;
            this.Direction = DirectionEnum.East;
        }

        private void MoveLeft()
        {
            var newX = this.XCoordinate == 0 ? this.GridSize : this.XCoordinate - 1;
            this.DetectObstacles((newX, this.YCoordinate));
            this.XCoordinate = newX;
            this.Direction = DirectionEnum.West;
        }

        private void DetectObstacles((int, int) nextMove)
        {
            if (!this.Obtacles.Any())
            {
                return;
            }

            var hasObtacles = this.Obtacles.Any(x => x == nextMove);
            if (hasObtacles)
            {
                throw new ObstacleDetectedException($"Obstacle is detected. Cannot proceed. CurrentLocation {this.GetCurrentCoordinates()}.");
            }
        }
    }
}
