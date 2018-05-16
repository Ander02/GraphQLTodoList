# GraphQL Todo List Server - Coisas a serem melhoradas 

## Exceptions e Erros
	
* Formas mais eficientes de tratar exceçães no sistema sem um try...catch em todo resolve

### Solução encontrada

* Método de Extensão de ResolveFieldContext para tratar e adicionar exceções ao context errors, baseado na própria implementação do graphQL.net alpha
>~~~C#
>public static async Task<object> TryResolveAsync(this ResolveFieldContext<object> context, Func<ResolveFieldContext<object>, Task<object>> resolve, Func<ExecutionErrors, Task<object>> error = null)
>{
>	try
>	{
>		return await resolve(context);
>	}
>	catch (Exception ex)
>   {
>		if (error == null)
>		{
>			context.Errors.AddGraphQLExceptions(ex);
>
>			return null;
>		}
>       return error(context.Errors);
>	}
>}
>~~~

* Uso 
>~~~C#
>FieldAsync(
>	...
>	resolve: async (context) => await context.TryResolveAsync(async (resolveContext) =>
>	{
>		//Faz algo que pode soltar uma exceção
>	})
>)
>~~~

## Validação

* Formas mais eficientes de tratar problemas de validação, usando fluent validator ou outra biblioteca

## Autenticação

* Formas de se fazer autenticação no sistema

## Autorização

* Formas de se dar diferentes permissões para campos no sistema

## Consultas performáticas

* Formas de se realizar consultas mais eficientes, otimizando as consultas apenas para os campos a serem buscados

## Arquitetura de Arquivos

* Definir uma arquitetura de pastas e arquivos que facilite o entendimento e navegação no código

## Relação dos tipos com Results, Querys e Commands

* Criar uma forma mais eficiente de mapear os Results, Querys e Commands para GraphQL Types

## Exemplo de CRUD

* Definir um exemplo de CRUD com relacionamento simples, que servirá de base para soluções mais complexas

## Upload de arquivos

* Formas eficientes de se realizar um file upload