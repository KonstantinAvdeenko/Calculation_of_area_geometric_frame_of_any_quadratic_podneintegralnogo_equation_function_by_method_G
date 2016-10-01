This program is in the C # programming language,
produces finding the area of ​​a figure using a certain integral by the Gauss method,
where the limits of a certain integral,
all quadratic function of the form t1 t2 * x t3 * x * x, t1 / t2 * x * t3 * x * x including
The arithmetic signs "", "-", "*" and "/" are entered by the user.
The user can also enter any incomplete quadratic function of the form t2 * x t3 * x * x; t2 * x etc.
necessarily entering the place of the empty term of the equation "0". The number of powers of polynomials points
into which the figure is divided to more accurately find the area is equal to the difference between the upper and lower limits,
which is fully acceptable for this method.

The main module IntegrirovanieMetodomGaussa contains the main function:
main - the task of entering the upper and lower limits of the user,
checking the upper and lower limits for compliance with the rule,
as well as the console output of the area already found from the GaussLegendre method.

Methods with which the main function works:
1) Gauss formula for finding the area of ​​the integrand GaussLegendre -
calculates the function of the integrand equation, which is taken from the f2 (x) method using
a weight-appropriate interpolation polynomial using the formula taken from the LegendreNodesWeights method;
2) The formula for finding the Lagrange polynomial of degree n at x Legendre -
evaluates to belong to one of the types of the Lagrange polynomial of degree n at the point x,
selected by the LegendreNodesWeights method.
For polynomials of type greater than one, the integrand from the f2 (x) method is taken into the formula;
3) Selection of suitable Lagrange polynomial point LegendreNodesWeights -
selects the appropriate Lagrange polynomial points, based on the possible weights when drawing lines through these points,
selects the desired axial quarter, then the appropriate point and polynomial for it, where the degree of the polynomial
returns from the Legendre method;
4) The integrand function of the equation f2 (x) is responsible for calculating the result of the function of the equation,
taking into account the data entered by the user about the function, and the transfer of this result to the GaussLegendre method for
the calculation of this function by the Gauss formula and the Legendre method for calculating by the 
formula polynomials of type greater than one;