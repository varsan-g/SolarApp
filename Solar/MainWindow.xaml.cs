using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Solar 
{
    public partial class MainWindow : Window
    {
        // Note: Best practice is not having this code in the code-behind UI-layer but instead structuring it in ex. a MVVM pattern.
        private DispatcherTimer dispatcherTimer;
        private Timer threadingTimer;

        private double 
            mercuryAngle, 
            venusAngle, 
            earthAngle, 
            marsAngle, 
            jupiterAngle, 
            saturnAngle, 
            uranusAngle, 
            neptuneAngle;

        private const double // Distances are approximates. Check the report for the explanation.
            mercuryRadius = 58, 
            venusRadius = 108, 
            earthRadius = 150, 
            marsRadius = 228, 
            jupiterRadius = 778, 
            saturnRadius = 1427, 
            uranusRadius = 2871, 
            neptuneRadius = 4495;


        public MainWindow()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Tick += TimerTick;

            threadingTimer = new Timer(ThreadingTimerTick);

            dispatcherTimer.Start();
            InitializeComponent();
          
        // We set the initial position of the sun and its label
            Canvas.SetLeft(sunLabel, 380); 
            Canvas.SetTop(sunLabel, 350 - sun.Height / 2);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdatePlanets();
        }

        private void ThreadingTimerTick(object state)
        {
            Dispatcher.Invoke(UpdatePlanets);
        }

        private void UpdatePlanets()
        {
            mercuryAngle += 0.024;
            venusAngle += 0.010;
            earthAngle += 0.008;
            marsAngle += 0.006;
            jupiterAngle += 0.002;
            saturnAngle += 0.001;
            uranusAngle += 0.0006;
            neptuneAngle += 0.0005;

            UpdatePlanet(mercury, mercuryRadius, mercuryAngle);
            UpdatePlanet(venus, venusRadius, venusAngle);
            UpdatePlanet(earth, earthRadius, earthAngle);
            UpdatePlanet(mars, marsRadius, marsAngle);
            UpdatePlanet(jupiter, jupiterRadius, jupiterAngle);
            UpdatePlanet(saturn, saturnRadius, saturnAngle);
            UpdatePlanet(uranus, uranusRadius, uranusAngle);
            UpdatePlanet(neptune, neptuneRadius, neptuneAngle);


            UpdatePlanetLabel(mercury, mercuryLabel, mercuryRadius, mercuryAngle);
            UpdatePlanetLabel(venus, venusLabel, venusRadius, venusAngle);
            UpdatePlanetLabel(earth, earthLabel, earthRadius, earthAngle);
            UpdatePlanetLabel(mars, marsLabel, marsRadius, marsAngle);
            UpdatePlanetLabel(jupiter, jupiterLabel, jupiterRadius, jupiterAngle);
            UpdatePlanetLabel(saturn, saturnLabel, saturnRadius, saturnAngle);
            UpdatePlanetLabel(uranus, uranusLabel, uranusRadius, uranusAngle);
            UpdatePlanetLabel(neptune, neptuneLabel, neptuneRadius, neptuneAngle);

        }
    // method with formula to update the pos of each respective planet
        private void UpdatePlanet(Ellipse planet, double radius, double angle)
        {
            Canvas.SetLeft(planet, 350 + radius * Math.Cos(angle) - planet.Width / 2);
            Canvas.SetTop(planet, 350 + radius * Math.Sin(angle) - planet.Height / 2);
        }

    // updates the pos for the labels
        private void UpdatePlanetLabel(Ellipse planet, Label label, double radius, double angle)
        {
            double labelOffset = planet.Width / 2 + 10; 
            Canvas.SetLeft(label, 350 + radius * Math.Cos(angle) + labelOffset);
            Canvas.SetTop(label, 350 + radius * Math.Sin(angle) - planet.Height / 2);
        }

    // stops DispatcherTimer and starts Threading Timer
        private void TimerToggle_Checked(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            threadingTimer.Change(0, 20);
        }
    // stops threading time for an infinite amount of time and starts the Dispatcher Timer
        private void TimerToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            threadingTimer.Change(Timeout.Infinite, 100);
            dispatcherTimer.Start();
        }
    }
}
