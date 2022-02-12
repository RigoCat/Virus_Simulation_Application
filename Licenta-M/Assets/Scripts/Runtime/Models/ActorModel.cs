namespace MF
{
    public class ActorModel 
    {
        public string Guid;

        public Observable<float> Health = new Observable<float>();

        public Observable<float> Energy = new Observable<float>();

        public Observable<float> Hunger = new Observable<float>();

        public Observable<float> Money = new Observable<float>();

        public Observable<float> MovementSpeed = new Observable<float>();

        public Observable<float> WalkSpeed = new Observable<float>();

        public Observable<float> RunSpeed = new Observable<float>();
    }
}
