using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace TaskMangement
{
    public class Task
    {
        public int TaskNumber { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime Date { get; set; }
        public int TimeExcution { get; set; }
    }

    public class CompletedTask
    {
        public Task TaskData { get; set; }
        public CompletedTask Next { get; set; }

        public CompletedTask(Task Taskdata)
        {
            TaskData = Taskdata;
            Next = null;

        }
    }
    public class TaskManeger
    {
        Task[] tasks = new Task[100];

        int TaskCount = 0;

        public CompletedTask Head = null;


        public void AddTask( int number ,string name, int priority, DateTime date,int time)

        {
            if (TaskCount >= tasks.Length)
            {
                Console.WriteLine("can not add more task the array is full");
                return;
            }
            else
            {
                var newTask = new Task
                {
                    TaskNumber = number,
                    Name = name,
                    Priority = priority,
                    Date = date
                    ,TimeExcution=time, 
                };

                tasks[TaskCount] = newTask;
                TaskCount++;

                Console.WriteLine("The task has been added successfully");

            }

        }
        public void DisplayTasks()
        {
            if (TaskCount == 0)
            {
                Console.WriteLine("There are no tasks to display");
                return;
            }
            else
            {
                for (int i = 0; i < TaskCount; i++)
                {
                    Console.WriteLine($"TaskNumber:{tasks[i].TaskNumber},Name:{tasks[i].Name},Priority:{tasks[i].Priority},Date:{tasks[i].Date.ToShortDateString()},TimeExcution:{tasks[i].TimeExcution}");
                }
            }
        }
        public void DeleteTask( int num) {
            int index = 0;
            for (int i = 0; i < TaskCount; i++)
            {
                if (tasks[i].TaskNumber == num)
                {
                    index = i;
                    break;
                }
            }
            if (index == 0)
            {
                Console.WriteLine("the task was not found");
                return;
            }
            for (int i = index; i < TaskCount-1; i++)
            {
                tasks[i] = tasks[i + 1];
            }
            TaskCount--;
            tasks[TaskCount-1] = null;
            Console.WriteLine("the task  is deleted");


        }
       

        public void SortByPriority()
        {
            for (int i = 0; i < TaskCount; i++)
            {
                for (int j = 0; j < TaskCount - 1; j++)
                {
                    if (tasks[j].Priority > tasks[j + 1].Priority)
                    {
                        var temp = tasks[j];
                        tasks[j] = tasks[j + 1];
                        tasks[j + 1] = temp;
                    }
                }
                Console.WriteLine($"TaskNumber:{tasks[i].TaskNumber},Name:{tasks[i].Name},Priority:{tasks[i].Priority},Date:{tasks[i].Date.ToShortDateString()},TimeExcution:{tasks[i].TimeExcution}");
            }
            
            Console.WriteLine("Sorting tasks is done");
        }

        public void SortByDate()
        {
            QuickSort(tasks, 0, TaskCount - 1);
            Console.WriteLine("sorting task is done ");
        }

        private void QuickSort(Task[] tasks, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(tasks, left, right);
                QuickSort(tasks, left, pivot - 1);
                QuickSort(tasks, pivot + 1, right);
            }
        }

        private int Partition(Task[] tasks, int left, int right)
        {
            DateTime pivot = tasks[right].Date;
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (tasks[j].Date <= pivot)
                {
                    i++;
                    var temp = tasks[i];
                    tasks [i] = tasks[j];
                    tasks[j] = temp;
                }
            }

            var temp1 = tasks[i + 1];
            tasks[i + 1] = tasks[right];
            tasks[right] = temp1;

            return i + 1;
            Console.WriteLine($"TaskNumber:{tasks[i].TaskNumber},Name:{tasks[i].Name},Priority:{tasks[i].Priority},Date:{tasks[i].Date.ToShortDateString()},TimeExcution:{tasks[i].TimeExcution}");
        }

        public void CompleteTask(int id)
        {
            Task taskToComplete = null;
            int index = 0;

            for (int i = 0; i < TaskCount; i++)
            {
                if (tasks[i].TaskNumber == id)
                {
                    taskToComplete = tasks[i];
                    index = i;
                    break;
                }
            }

            if (taskToComplete == null)
            {
                Console.WriteLine(" the task was not found.");
                return;
            }

        

         for (int i = index; i<TaskCount - 1; i++)
            {
                tasks[i] = tasks[i + 1];
            }
        TaskCount--;

           
            var newNode = new CompletedTask(taskToComplete);
            if( Head == null)
            {
                Head = newNode;
            }
            else
            {
                var current = Head;
               while (current.Next != null)
              {
                  current = current.Next;
                }
              current.Next = newNode;
              }

          Console.WriteLine(" the task has been transferred to completed task");
        }

        public void DisplayCompletedTasks()
        {
            if (Head == null)
            {
                Console.WriteLine(" completed task is not found");
                return;
            }

           
            var current = Head;
            while (current != null)
            {
                Console.WriteLine($": TaskNumber{current.TaskData.TaskNumber}, name: {current.TaskData.Name}, priority: {current.TaskData.Priority} ,  Date: {current.TaskData.Date.ToShortDateString()}, TimeExcution: {current.TaskData.TimeExcution}");
                current = current.Next;
            }
        }

         



              public void SortByTimeExecution()
        {
            for (int i = 0; i < TaskCount - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < TaskCount; j++)
                {
                    if (tasks[j].TimeExcution< tasks[minIndex].TimeExcution)
                    {
                        minIndex = j;
                    }
                }

                // Swap the entire task objects
                Task tempTask = tasks[minIndex];
                tasks[minIndex] = tasks[i];
                tasks[i] = tempTask;

                Console.WriteLine($"TaskNumber:{tasks[i].TaskNumber},Name:{tasks[i].Name},Priority:{tasks[i].Priority},Date:{tasks[i].Date.ToShortDateString()},TimeExcution:{tasks[i].TimeExcution}");
            }
        }










        static void Main()

        {
            TaskManeger mange = new TaskManeger();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("*** Task Mangement System***");
                Console.WriteLine("1.Add anew task");
                Console.WriteLine("2.Display all tasks");
                Console.WriteLine("3.Delete a task");
                Console.WriteLine("4.Sort tasks by priority");
                Console.WriteLine("5.Sort tasks by date");
                Console.WriteLine("6.Complete a task");
                Console.WriteLine("7.Display completed tasks");
                Console.WriteLine("8.Sort tasks by Time Excution");
                Console.WriteLine("9.Exit");

                Console.WriteLine("Enter your choice ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case"1":
                        Console.WriteLine("Enter task TaskNumber:");
                        int num =  int.Parse(Console.ReadLine());


                        Console.WriteLine("Enter task name:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter task priority:1.High 2.medium 3.low:");
                        int priority = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter task date: yyyy/mm/dd:");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Enter task time:");
                        int time= int.Parse(Console.ReadLine());

                        mange.AddTask( num,name, priority, date,time);

                        break;

                    case"2":
                        mange.DisplayTasks();
                        break;
                    case "3":
                        Console.WriteLine("Enter task num:");
                        int number = int.Parse(Console.ReadLine());
                        mange.DeleteTask(number);
                        break;
                    case "4":
                        mange.SortByPriority();
                        break;
                    case "5":
                        mange.SortByDate();
                        break;
                    case "6":
                        Console.WriteLine("Enter task ID completad:");
                        int IDcompleted = int.Parse(Console.ReadLine());
                        mange.CompleteTask(IDcompleted);
                        break;
                    case "7":
                        mange.DisplayCompletedTasks();
                        break;
                    case "8":
                        mange.SortByTimeExcution();
                        break;

                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("invalid enter!");
                        break;



                }
            }
        }

    }
}

