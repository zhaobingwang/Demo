namespace csharp Thrift.Client

struct OPNum{
	1:double Num1;
	2:double Num2;
}
enum EnumOP{
	Add=1,
	Sub=2,
	Mult=3,
	Div=4,
}
service MathService{
	i32 add (1:i32 a,2:i32 b);
	double Compute (1:EnumOP op,2:OPNum num);
}