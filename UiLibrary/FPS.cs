namespace UILibrary
{
    public sealed class FPS
    {
        public long TotalFrames
        {
            get;
            private set;
        }

        public float TotalSeconds
        {
            get;
            private set;
        }

        public float AvgFps
        {
            get;
            private set;
        }

        public float CurFps
        {
            get;
            private set;
        }

        public const int MaximumSamples = 100;
        private readonly Queue<float> sampleBuffer;

        public void Update(float deltaTime)
        {
            CurFps = 1.0f / deltaTime;
            sampleBuffer.Enqueue(CurFps);

            if (sampleBuffer.Count > MaximumSamples)
            {
                sampleBuffer.Dequeue();
                AvgFps = sampleBuffer.Average(i => i);
            }
            else
            {
                AvgFps = CurFps;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
        }

        public FPS()
        {
            sampleBuffer = new();
        }
    }
}