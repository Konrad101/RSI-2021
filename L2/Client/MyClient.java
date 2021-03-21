public class MyClient {
    public static void main(String[] args) {
        if (args.length == 0) {
            System.out.println("You have to enter RMI object address in" +
                    " the form: //host_address/service_name");
            return;
        }

        executeFirstCalculate(args[0]);
        executeSecondCalculate(args[1]);
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
}
