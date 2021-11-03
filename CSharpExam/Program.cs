using System;

/// <summary>
/// 계산 결과 출력하는 프로그램
/// 모드 2가지 선택 가능하도록 (배열 모드, 숫자 2개 계산하는 모드)
/// 배열 모드 선택 시 숫자 원하는 만큼 입력 받아서 한번에 계산(덧셈, 뺄셈, 곱셈, 나눗셈) 하여 결과 값 출력
/// 숫자 모드 선택 시 숫자 2개, 구분자 입력 받아 결과 값 출력
/// 모든 변수는 프로퍼티 이용 (숫자는 -10000 ~ 10000 까지만 입력 받고 범위 넘어갈 경우 예외처리)
/// 변수 타입은 var, dynamic, object, 일반 변수 type 사용 (차이점 비교 해 보기)
/// 입력 값이 잘못 될 경우 예외처리 필수!
/// 연산은 Calc 클래스에서 진행 하고 콘솔창 출력은 Display 클래스에서 진행
/// </summary>
/// 
namespace CSharpExam
{
    class Program
    {
        static void Main(string[] args)
        {
            Exam1 exam1 = new Exam1();
            exam1.Run();

            Exam2 exam2 = new Exam2();
            exam2.Run();
        }
    }

    // 계산 결과 출력 프로그램
    class Exam1
    {
        public int calcMode { get; set; }
        public void Run()
        {
            Console.WriteLine("================계산기================");
            while (true)
            {
                try
                {
                    Console.WriteLine("1: 배열 모드, 2: 숫자 2개 계산하는 모드");
                    string sCalcMode = Console.ReadLine();
                    calcMode = Convert.ToInt32(sCalcMode);
                }
                catch (Exception e) // 예외처리
                {
                    Console.WriteLine("정수를 입력해주세요.");
                    continue;
                }


                if (calcMode != 1 && calcMode != 2)
                {
                    Console.WriteLine("올바른 수를 다시 입력해주세요.");
                }
                else
                {
                    Calc calc = new Calc(calcMode);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Calc Class 생성 필요 (연산)
    /// </summary>
    public class Calc
    {
        int _number1, _number2;
        string sInput { get; set; }
        // 배열 모드 사용
        public string[] list { get; set; }
        public int[] numList { get; set; }
        // 숫자 2개 모드에 사용

        public int number1
        {
            get => _number1;
            set
            {
                if (value > -10000 && value < 10000) _number1 = value;
                else
                {
                    throw new Exception();
                }
            }

        }
        public int number2
        {
            get => _number2;
            set
            {
                if (value > -10000 && value < 10000) _number2 = value;
                else
                {
                    throw new Exception();
                }
            }
        }

        char op { get; set; }

        public Calc(int calcMode)
        {
            if (calcMode == 1) // 배열 모드
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("배열 모드 진입, 숫자들을 스페이스로 구분하여 순서대로 입력");
                        sInput = Console.ReadLine();
                        list = sInput.Split(' ');
                        numList = Array.ConvertAll(list, int.Parse);

                        Console.WriteLine("Plus = {0}, Minus = {1}, Multiply = {2}, Divide = {3}", Plus(numList), Minus(numList), Multiply(numList), Divide(numList));
                        break;
                    }
                    catch (DivideByZeroException e)
                    {
                        Console.WriteLine("0으로 나눌 수 없습니다.");
                        continue;
                    }
                    catch (Exception e) // 예외처리
                    {
                        Console.WriteLine("다시 올바르게 입력해주세요.");
                        continue;
                    }
                }
            }
            else // 숫자 2개 계산하는 모드
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("숫자 2개 계산하는 모드 진입, 숫자 2개와 연산자를 스페이스로 구분하여 순서대로 입력");
                        sInput = Console.ReadLine();
                        list = sInput.Split(' ');
                        number1 = Convert.ToInt32(list[0]);
                        number2 = Convert.ToInt32(list[1]);
                        op = ((string)list[2])[0];
                        if (Calculator(number1, number2, op) == -1)
                        {
                            Console.WriteLine("올바른 연산자를 입력해주세요.");
                            continue;
                        }
                        Console.WriteLine(Calculator(number1, number2, op));
                        break;
                    }
                    catch (DivideByZeroException e)
                    {
                        Console.WriteLine("0으로 나눌 수 없습니다.");
                        continue;
                    }
                    catch (Exception e) // 예외처리
                    {
                        Console.WriteLine("다시 올바르게 입력해주세요. 숫자 2개와 연산자 이외 다른 것을 입력해서는 안되며, 숫자의 범위는 -10000에서 10000 사이여야 합니다.");
                        continue;
                    }
                }

            }
        }

        int Plus(params int[] list)
        {
            var result = 0;
            foreach (int i in list)
            {
                result += i;
            }
            return result;
        }
        int Divide(params int[] list)
        {
            int result = 0;
            foreach (int i in list)
            {
                result /= i;
            }
            return result;
        }
        int Minus(params int[] list)
        {
            dynamic result = 0;
            foreach (int i in list)
            {
                result -= i;
            }
            return result;
        }
        int Multiply(params int[] list)
        {
            int result = 1;
            foreach (int i in list)
            {
                result *= i;
            }
            return result;
        }

        int Calculator(int number1, int number2, char op)
        {
            switch (op)
            {
                case '+':
                    return number1 + number2;
                case '-':
                    return number1 - number2;
                case '*':
                    return number1 * number2;
                case '/':
                    return number1 / number2;
            }
            return -1; // 구분자 오류
        }
        // 필요 함수
        // foreach 이용
        // Plus(params 형태의 배열을 매개변수로), Return 값은 int
        // Devide(params 형태의 배열을 매개변수로), Return 값은 int
        // Minus(params 형태의 배열을 매개변수로), Return 값은 int
        // Multiply(params 형태의 배열을 매개변수로), Return 값은 int
        // Calculator(number1, number2, 구분자('+', '-', '*', '/'), number3(결과값 ref나 out 이용))
    }

    /// <summary>
    /// Display Class 생성 필요 (화면 출력)
    /// Display Class에서 System.Console 클래스 기능 사용 가능하도록 구현
    /// </summary>
    public class Display
    {
        int input;
        int size;
        public int ReadLine()
        {
            return int.Parse(Console.ReadLine());
        }
        public void WriteLine()
        {
            Console.WriteLine("================도형================");
            Console.WriteLine("Triangle: 0");
            Console.WriteLine("Circle: 1");
            Console.WriteLine("Rectangle: 2");
            Console.Write("Please Input number: ");

            while (true)
            {
                try
                {
                    Console.Write("Please Input number: ");
                    input = Convert.ToInt32(ReadLine());
                    Console.Write("Please Input size: ");
                    size = Convert.ToInt32(ReadLine());
                    if (size < 0)
                        throw new Exception();
                    break;
                }

                catch (Exception e)
                {
                    Console.WriteLine("유효하지 않은 숫자입니다.");
                    continue;
                }

            }


            Exam2.Shape S;

            switch (input)
            {
                case 0:
                    S = new Exam2.Triangle(size);
                    S.Draw();
                    break;
                case 1:
                    S = new Exam2.Circle(size);
                    S.Draw();
                    break;
                case 2:
                    S = new Exam2.Rectangle(size);
                    S.Draw();
                    break;
                default:
                    break;
            }

        }
    }

    // 도형 출력 프로그램
    // Size를 입력 받아 콘솔창에 도형을 그려주면 됨
    class Exam2
    {
        public void Run()
        {
            Display d = new Display();
            d.WriteLine();
        }

        public enum ShapeType
        {
            Triangle,
            Circle,
            Rectangle
        };

        /// <summary>
        /// 부모 Class
        /// </summary>
        public abstract class Shape
        {
            protected int Size { get; private set; }

            public Shape(int size)
            {
                Size = size;
            }
            public virtual void Draw()
            {
            }
        }

        // 자식 클래스(Triangle, Circle, Rectangle) 생성 필요
        // Shape 클래스를 상속 받아 Draw 함수 구현
        // Draw 함수는 Display 클래스로 호출

        public class Triangle : Shape
        {
            public Triangle(int Size) : base(Size) { }
            override public void Draw()
            {
                for (int i = 0; i < Size; ++i)
                {
                    for (int j = 0; j < Size - i - 1; ++j)
                    {
                        Console.Write(" ");
                    }
                    for (int j = 0; j < 2 * i + 1; ++j)
                    {
                        Console.Write("#");
                    }
                    Console.WriteLine();
                }
            }
        }
        public class Circle : Shape
        {
            public Circle(int Size) : base(Size) { }
            override public void Draw() // 원은 그냥 네모로 그림
            {
                for (int i = 0; i < Size; ++i)
                {
                    for (int j = 0; j < Size; ++j)
                    {
                        Console.Write("#");
                    }
                    Console.WriteLine();
                }
            }
        }

        public class Rectangle : Shape
        {
            public Rectangle(int Size) : base(Size) { }
            override public void Draw()
            {
                for (int i = 0; i < Size; ++i)
                {
                    for (int j = 0; j < Size; ++j)
                    {
                        Console.Write("#");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
