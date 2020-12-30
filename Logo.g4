grammar Logo;


program returns [Sequence c]
    : command_list EOF;
    
command_list returns [Sequence c]
    : command rest=command_list
        {
            $ctx.c = $rest.c;
            $ctx.c.Commands.Insert(0, $command.ctx.c); 
        }
    | { $ctx.c = new Sequence(new List<ICommand>{}); }
    ;

command returns [ICommand c]
    :   'FD' value=num { $ctx.c = new Move($value.n, Direction.Forward); }
    |   'RT' value=num { $ctx.c = new Move($value.n, Direction.Right); }
    |   'BK' value=num { $ctx.c = new Move($value.n, Direction.Backward); }
    |   'LT' value=num { $ctx.c = new Move($value.n, Direction.Left); }
    |   'R' value=num { $ctx.c = new Rotate($value.n); }
    |   'REPEAT' ntimes=num '[' command_list ']' { $ctx.c = new Repeat($ntimes.n, $command_list.ctx.c); }
    |   'PU' { $ctx.c = new SetPen(Pen.Up); }
    |   'PD' { $ctx.c = new SetPen(Pen.Down); }
    |   'PE' { $ctx.c = new SetPen(Pen.Erase); }
    ;

num returns [int n]
    : v=INT { $ctx.n = $v.int; }
    | '(' p=num ')' { $ctx.n = $p.n; }
    | '-' a=num { $ctx.n = - $a.n; }
    | a=num '+' b=num { $ctx.n = $a.n + $b.n; }
    | a=num '-' b=num { $ctx.n = $a.n - $b.n; }
    | a=num '*' b=num { $ctx.n = $a.n * $b.n; }
    | a=num '/' b=num { $ctx.n = $a.n / $b.n; }
    ;

INT: [0-9]+;
WS : [ \t\r\n]+ -> skip ;  