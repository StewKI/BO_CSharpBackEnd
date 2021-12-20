
SmartToDo App = new SmartToDo(10);

App.AddTask(new Task("1","", new Interval(DateTime.Today, 10,0,11,0)));
App.AddTask(new Task("2","", new Interval(DateTime.Today, 12,0,13,0)));
App.AddTask(new Task("3","", new Interval(DateTime.Today, 14,0,15,0)));

var a = App.newTaskCheck(new Task("nt1","", new Interval(DateTime.Today, 12,00, 13,5)), new Interval(DateTime.Today, 1,00, 22,00));

foreach(var i in a){
  System.Console.WriteLine("TEST* " + i);
}