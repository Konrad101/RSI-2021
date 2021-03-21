import Task.IComputeTask;
import Task.Task;
import Task.TaskResult;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class MyClient {
    public static void main(String[] args) {
        if (args.length == 0) {
            System.out.println("You have to enter RMI object address in" +
                    " the form: //host_address/service_name");
            return;
        }

//        executeFirstCalculate(args[0]);
//        executeSecondCalculate(args[1]);
        testTaskComputation(args[0]);
    }

    private static void executeFirstCalculate(String address) {
        double result;
        CalcObject calcObject;

        try {
            calcObject = (CalcObject) java.rmi.Naming.lookup(address);
        } catch (Exception e) {
            System.out.println("Nie mozna pobrac referencji do " + address);
            e.printStackTrace();
            return;
        }
        System.out.println("Referencja do " + address + " jest pobrana.");

        try {
            result = calcObject.calculate(1.1, 2.2);
        } catch (Exception e) {
            System.out.println("Blad zdalnego wywolania.");
            e.printStackTrace();
            return;
        }

        System.out.println("Wynik = " + result);
    }

    private static void executeSecondCalculate(String address) {
        double result;
        CalcObject2 calcObject;

        try {
            calcObject = (CalcObject2) java.rmi.Naming.lookup(address);
        } catch (Exception e) {
            System.out.println("Nie mozna pobrac referencji do " + address);
            e.printStackTrace();
            return;
        }
        System.out.println("Referencja do " + address + " jest pobrana.");

        try {
            InputType inputType = new InputType(1.1, 2.2);
            inputType.operation = "sub";
            ResultType resultType = calcObject.calculate2(inputType);
            result = resultType.result;
            System.out.println(resultType.resultDescription);
        } catch (Exception e) {
            System.out.println("Blad zdalnego wywolania.");
            e.printStackTrace();
            return;
        }

        System.out.println("Wynik = " + result);
    }

    private static void testTaskComputation(String address) {
        int[] numbers = new int[]{1, 2, 3, 4, 5, 6, 7};
        Task task = new Task(numbers);
        task.operation = "nearestAvg";
        int nearestValue = 8;
        task.setNearestValue(nearestValue);
        TaskResult result = getResult(task, address);
        if (result != null) {
            System.out.println("Nearest value to " + nearestValue + ": " + result.getFirstPartOfResult());
            System.out.println("Deviation: " + result.getSecondPartOfResult());
        }

        task.operation = "minMax";
        result = getResult(task, address);
        if (result != null) {
            System.out.println("Min value: " + result.getFirstPartOfResult());
            System.out.println("Max value: " + result.getSecondPartOfResult());
        }
    }

    private static TaskResult getResult(Task task, String address) {
        IComputeTask computeTask;

        try {
            computeTask = (IComputeTask) java.rmi.Naming.lookup(address);
        } catch (Exception e) {
            System.out.println("Nie mozna pobrac referencji do " + address);
            e.printStackTrace();
            return null;
        }

        System.out.println("Referencja do " + address + " jest pobrana.");
        try {
            int[] numbers = task.getNumbers();
            List<Task> tasks = new ArrayList<>();
            int index = 0;
            int tasksAmount = 3;
            int numbersPerTask = numbers.length / tasksAmount;

            for (int i = 0; i < tasksAmount - 1; i++) {
                Task tempTask = new Task(Arrays.copyOfRange(numbers, index, index + numbersPerTask));
                tempTask.setNearestValue(task.getNearestValue());
                tasks.add(tempTask);
                index += numbersPerTask;
            }

            Task lastTask = new Task(Arrays.copyOfRange(numbers, index, numbers.length));
            lastTask.setNearestValue(task.getNearestValue());
            tasks.add(lastTask);

            List<TaskResult> results = new ArrayList<>();
            for (Task t : tasks) {
                t.operation = task.operation;
                results.add(computeTask.compute(t));
            }

            TaskResult finalResult;
            if (results.get(0).operation.equals("minMax")) {
                finalResult = getMinMaxResult(results);
            } else {
                finalResult = getNearestValueResult(results);
            }

            return finalResult;
        } catch (Exception e) {
            System.out.println("Blad zdalnego wywolania.");
            e.printStackTrace();
        }

        return null;
    }

    private static TaskResult getNearestValueResult(List<TaskResult> results) {
        TaskResult finalResult;
        int nearestValue = -1;
        int deviation = Integer.MAX_VALUE;
        for (TaskResult result : results) {
            int resultValue = result.getFirstPartOfResult();
            int resultDeviation = Math.abs(resultValue - result.getSecondPartOfResult());
            if (resultDeviation < deviation) {
                nearestValue = resultValue;
                deviation = resultDeviation;
            }
        }

        finalResult = new TaskResult("", nearestValue, deviation);
        return finalResult;
    }

    private static TaskResult getMinMaxResult(List<TaskResult> results) {
        TaskResult finalResult;
        int min = Integer.MAX_VALUE;
        int max = Integer.MIN_VALUE;
        for (TaskResult result : results) {
            int minResult = result.getFirstPartOfResult();
            int maxResult = result.getSecondPartOfResult();
            if (minResult < min) {
                min = minResult;
            }
            if (maxResult > max) {
                max = maxResult;
            }
        }

        finalResult = new TaskResult("", min, max);
        return finalResult;
    }

}
