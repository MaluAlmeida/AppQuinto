using AppQuinto.Models;
using AppQuinto.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppQuinto.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;

        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Update usuario set DateNasc = @DateNasc, nomeCli=@nomeCli," +
                                                    "Tel=@Tel, Cpf=@Cpf Where IdCli=@IdCli", conexao);

                cmd.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = cliente.Cpf;
                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = cliente.NomeCli;
                cmd.Parameters.Add("@Tel", MySqlDbType.VarChar).Value = cliente.Tel;
                cmd.Parameters.Add("@DateNasc", MySqlDbType.VarChar).Value = cliente.DateNasc.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into cliente(Cpf, nomeCli, Tel, DateNasc) " +
                                                    " values (@Cpf, @nomeCli, @Tel, @DateNasc)", conexao);
                cmd.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = cliente.Cpf;
                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = cliente.NomeCli;
                cmd.Parameters.Add("@Tel", MySqlDbType.VarChar).Value = cliente.Tel;
                cmd.Parameters.Add("@DateNasc", MySqlDbType.VarChar).Value = cliente.DateNasc.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from cliente where IdCli=@IdCli", conexao);
                cmd.Parameters.AddWithValue("@IdCli", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Cliente ObterCliente(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from cliente " +
                                                    "where IdCli = @IdCli", conexao);
                cmd.Parameters.AddWithValue("@IdCli", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.IdCli = Convert.ToInt32(dr["IdCli"]);
                    cliente.Cpf = (string)dr["Cpf"];
                    cliente.NomeCli = (string)(dr["NomeCli"]);
                    cliente.Tel = (string)(dr["Tel"]);
                    cliente.DateNasc = Convert.ToDateTime(dr["DateNasc"]);
                }
               
                return cliente;
            }
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> ClienteList = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Cliente", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    ClienteList.Add(
                        new Cliente
                        {
                            IdCli = Convert.ToInt32(dr["IdCli"]),
                            Cpf = (string)dr["Cpf"],
                            NomeCli = (string)dr["NomeCli"],
                            Tel = (string)dr["Tel"],
                            DateNasc = Convert.ToDateTime(dr["DateNasc"])
                        });
                }
                return ClienteList;
            }
        }

    }
}
