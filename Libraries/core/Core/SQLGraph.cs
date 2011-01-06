﻿/*

Copyright Robert Vesse 2009-10
rvesse@vdesign-studios.com

------------------------------------------------------------------------

This file is part of dotNetRDF.

dotNetRDF is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

dotNetRDF is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with dotNetRDF.  If not, see <http://www.gnu.org/licenses/>.

------------------------------------------------------------------------

dotNetRDF may alternatively be used under the LGPL or MIT License

http://www.gnu.org/licenses/lgpl.html
http://www.opensource.org/licenses/mit-license.php

If these licenses are not suitable for your intended use please contact
us at the above stated email address to discuss alternative
terms.

*/

#if !NO_DATA && !NO_STORAGE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using VDS.RDF.Parsing;
using VDS.RDF.Storage;

namespace VDS.RDF
{

    /// <summary>
    /// Class for representing an RDF Graph which is automatically stored to a backing SQL Store as it is modified
    /// </summary>
    /// <threadsafety instance="false">Safe for multi-threaded read-only access but unsafe if one/more threads may modify the Graph by using the <see cref="Graph.Assert">Assert</see>, <see cref="Graph.Retract">Retract</see> or <see cref="BaseGraph.Merge">Merge</see> methods</threadsafety>
    [Obsolete("Direct SQL Backed Stores are considered obsolete and are not recommended for anything other than small scale prototyping and will be removed in future versions", false)]   
    public class SqlGraph : Graph
    {
        /// <summary>
        /// Graph ID of the Graph in the Store
        /// </summary>
        protected String _graphID = String.Empty;
        /// <summary>
        /// Manager that manages the SQL IO
        /// </summary>
        protected ISqlIOManager _manager;
        /// <summary>
        /// Indicates whether database IO should be suspended, used during loading of the Graph
        /// </summary>
        protected bool _suspendIO = false;

        /// <summary>
        /// Empty Constructor for use by derived classes which don't wish to have the Graph automatically loaded
        /// </summary>
        protected SqlGraph() { }

        /// <summary>
        /// Creates a new instance of a Graph which is automatically saved to the given SQL Store, if a Graph with the Uri exists in the store it will be automatically loaded
        /// </summary>
        /// <param name="graphUri">Uri of the Graph</param>
        /// <param name="dbserver">Database Server</param>
        /// <param name="dbname">Database Name</param>
        /// <param name="dbuser">Database User</param>
        /// <param name="dbpassword">Database Password</param>
        /// <remarks>Assumes that the SQL Store is a dotNetRDF MS SQL Store</remarks>
        public SqlGraph(Uri graphUri, String dbserver, String dbname, String dbuser, String dbpassword)
            : this(graphUri, new MicrosoftSqlStoreManager(dbserver, dbname, dbuser, dbpassword))
        { }

        /// <summary>
        /// Creates a new instance of a Graph which is automatically saved to the given SQL Store, if a Graph with the Uri exists in the store it will be automatically loaded.  Assumes the Database is on the localhost
        /// </summary>
        /// <param name="graphUri">Uri of the Graph</param>
        /// <param name="dbname">Database Name</param>
        /// <param name="dbuser">Database User</param>
        /// <param name="dbpassword">Database Password</param>
        /// <remarks>Assumes that the SQL Store is a dotNetRDF MS SQL Store</remarks>
        public SqlGraph(Uri graphUri, String dbname, String dbuser, String dbpassword)
            : this(graphUri, "localhost", dbname, dbuser, dbpassword) { }

        /// <summary>
        /// Creates a new instance of a Graph which is automatically saved to the given SQL Store, if a Graph with the Uri exists in the store it will be automatically loaded
        /// </summary>
        /// <param name="graphUri">Uri of the Graph</param>
        /// <param name="manager">An <see cref="ISqlIOManager">ISqlIOManager</see> for your chosen underlying store</param>
        /// <remarks>The Store may be any database for which a working <see cref="ISqlIOManager">ISqlIOManager</see> has been defined</remarks>
        public SqlGraph(Uri graphUri, ISqlIOManager manager) : base()
        {
            //Set Database Mananger
            this._manager = manager;

            //Base Uri is the Graph Uri
            this.BaseUri = graphUri;

            //Get the Graph ID and then Load the Namespaces and Triples
            this._graphID = this._manager.GetGraphID(graphUri);
            this._manager.LoadNamespaces(this, this._graphID);
            this._suspendIO = true;
            this._manager.LoadTriples(this, this._graphID);
            this._suspendIO = false;

            //Subscribe to Namespace Map Events
            //This has to happen after we load from the Database as otherwise the Loading will fire off all the
            //Namespace Map events creating unecessary overhead
            this._nsmapper.NamespaceAdded += this.HandleNamespaceAdded;
            this._nsmapper.NamespaceModified += this.HandleNamespaceModified;
            this._nsmapper.NamespaceRemoved += this.HandleNamespaceRemoved;
        }

        /// <summary>
        /// Gets the <see cref="Storage.ISqlIOManager">ISqlIOManager</see> used to manage database IO for this Graph
        /// </summary>
        public ISqlIOManager Manager
        {
            get
            {
                return this._manager;
            }
        }

        /// <summary>
        /// Asserts a Triple into the Graph
        /// </summary>
        /// <param name="t">Triple to Assert</param>
        public override void Assert(Triple t)
        {
            base.Assert(t);

            //Use this special switch during loading to stop the extra overhead of going back to the Database all the time
            if (this._suspendIO) return;

            //Do extra Database Assertion
            this._manager.SaveTriple(t, this._graphID);
        }

        /// <summary>
        /// Asserts an Enumerable of Triples into the Graph
        /// </summary>
        /// <param name="ts">Triples to assert</param>
        public override void Assert(IEnumerable<Triple> ts)
        {
            this.Assert(ts.ToList());
        }

        /// <summary>
        /// Asserts a List of Triples into the Graph
        /// </summary>
        /// <param name="ts">Triples to assert</param>
        public override void Assert(List<Triple> ts)
        {
            this._manager.Open(true);
            foreach (Triple t in ts)
            {
                this.Assert(t);
            }
            this._manager.Close(true);
        }

        /// <summary>
        /// Asserts an Array of Triples into the Graph
        /// </summary>
        /// <param name="ts">Triples to assert</param>
        public override void Assert(Triple[] ts)
        {
            this._manager.Open(true);
            foreach (Triple t in ts)
            {
                this.Assert(t);
            }
            this._manager.Close(true);
        }

        /// <summary>
        /// Retracts a Triple from the Graph
        /// </summary>
        /// <param name="t">Triple to Retract</param>
        public override void Retract(Triple t)
        {
            base.Retract(t);

            //Do extra Database Retraction
            this._manager.Open(false);
            this._manager.RemoveTriple(t, this._graphID);
            this._manager.Close(false);
        }

        /// <summary>
        /// Retracts an Enumerable of Triples from the Graph
        /// </summary>
        /// <param name="ts">Triples to Retract</param>
        public override void Retract(IEnumerable<Triple> ts)
        {
            this.Retract(ts.ToList());
        }

        /// <summary>
        /// Retracts a List of Triples from the Graph
        /// </summary>
        /// <param name="ts">Triples to Retract</param>
        public override void Retract(List<Triple> ts)
        {
            this._manager.Open(true);
            foreach (Triple t in ts)
            {
                this.Retract(t);
            }
            this._manager.Close(true);
        }

        /// <summary>
        /// Retracts an Array of Triples from the Graph
        /// </summary>
        /// <param name="ts">Triples to Retract</param>
        public override void Retract(Triple[] ts)
        {
            this._manager.Open(true);
            foreach (Triple t in ts)
            {
                this.Retract(t);
            }
            this._manager.Close(true);
        }

        /// <summary>
        /// Causes the Graph to be refreshed from the Database
        /// </summary>
        public virtual void Refresh()
        {
            //Clear Triples and Nodes
            this._triples.Dispose();
            this._nodes.Dispose();

            //Clear the Namespace Map
            this._nsmapper.Clear();

            //Temporarily disable handling of the NamespaceAdded event
            this._nsmapper.NamespaceAdded -= this.HandleNamespaceAdded;

            //Reload Namespace Map
            this._manager.LoadNamespaces(this,this._graphID);

            //Reload Triples
            this._suspendIO = true;
            this._manager.LoadTriples(this,this._graphID);
            this._suspendIO = false;

            //Handle the NamespaceAdded event again
            this._nsmapper.NamespaceAdded += this.HandleNamespaceAdded;
        }

        /// <summary>
        /// Internal Handler for the NamespaceAdded Event of the Namespace map
        /// </summary>
        /// <param name="prefix">Namespace Prefix</param>
        /// <param name="uri">Namespace Uri</param>
        protected void HandleNamespaceAdded(String prefix, Uri uri)
        {
            this._manager.SaveNamespace(prefix, uri, this._graphID);
        }

        /// <summary>
        /// Internal Handler for the NamespaceModified Event of the Namespace map
        /// </summary>
        /// <param name="prefix">Namespace Prefix</param>
        /// <param name="uri">Namespace Uri</param>
        protected void HandleNamespaceModified(String prefix, Uri uri)
        {
            this._manager.UpdateNamespace(prefix, uri, this._graphID);
        }

        /// <summary>
        /// Internal Handler for the NamespaceRemoved Event of the Namespace map
        /// </summary>
        /// <param name="prefix">Namespace Prefix</param>
        /// <param name="uri">Namespace Uri</param>
        protected void HandleNamespaceRemoved(String prefix, Uri uri)
        {
            this._manager.RemoveNamespace(prefix, uri, this._graphID);
        }

        /// <summary>
        /// Disposes of a SQL Graph
        /// </summary>
        public override void Dispose()
        {
            this._manager.Dispose();
            base.Dispose();
        }
    }
}

#endif
